import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Alert, Box, Button, Card, CardContent, Chip, Collapse, MenuItem, Paper, TextField, Typography } from '@mui/material';
import { isAxiosError } from 'axios';
import { useNavigate } from 'react-router-dom';
import PageHeader from '../../../components/PageHeader';
import { planAiAction, type ActionDraft, type AiActionPlan } from '../../../api/aiApi';
import { changeVehicleStatus, createFuelRecord, createMaintenanceRecord, createTrip, fetchDrivers, fetchVehicles, setVehicleAssignment } from '../../../api/fleetApi';
import type { Driver, Vehicle } from '../../../api/types';
import { canAssignVehicleDriver, canChangeVehicleStatus, canCreateFuel, canCreateMaintenance, canCreateTrip } from '../../../auth/rolePermissions';

const prompts = [
  'Schedule an oil change for VH-001 next Monday and book a trip from Mumbai to Delhi for Driver Ahmed on Tuesday.',
  'Log fuel for VH-001: 50 litres, cost 5000, odometer 25000.',
  'Change VH-001 status to maintenance and assign driver@example.com to VH-002.',
];
type DraftValues = Record<string, string | boolean>;
type ActionState = { status: 'Draft' | 'Confirming' | 'Done' | 'Failed'; error?: string };
const actionLabels: Record<ActionDraft['action'], string> = {
  scheduleTrip: 'Trip', createMaintenance: 'Maintenance', logFuel: 'Fuel log',
  changeVehicleStatus: 'Vehicle status', assignDriver: 'Driver assignment', none: 'Information',
};
const fieldsByAction: Record<ActionDraft['action'], string[]> = {
  scheduleTrip: ['vehicleNumber', 'driverEmail', 'source', 'destination', 'startDate', 'endDate'],
  createMaintenance: ['vehicleNumber', 'serviceType', 'description', 'scheduledDate', 'cost', 'vendor'],
  logFuel: ['vehicleNumber', 'fuelDate', 'quantity', 'cost', 'odometerReading'],
  changeVehicleStatus: ['vehicleNumber', 'newStatus', 'reason'],
  assignDriver: ['vehicleNumber', 'driverEmail', 'unassign'], none: [],
};
const draftData = (draft: ActionDraft): DraftValues => {
  const value = draft[draft.action === 'scheduleTrip' ? 'trip' : draft.action === 'createMaintenance' ? 'maintenance' : draft.action === 'logFuel' ? 'fuel' : draft.action === 'changeVehicleStatus' ? 'vehicleStatus' : 'driverAssignment'];
  return Object.fromEntries(fieldsByAction[draft.action].map((field) => [field, String((value as Record<string, unknown> | null)?.[field] ?? '')]));
};
const labelFor = (field: string) => ({ vehicleNumber: 'Vehicle', driverEmail: 'Driver email', source: 'Source', destination: 'Destination', startDate: 'Start', endDate: 'End', serviceType: 'Service type', description: 'Description', scheduledDate: 'Scheduled date', cost: 'Cost', vendor: 'Vendor', fuelDate: 'Fuel date', quantity: 'Quantity', odometerReading: 'Odometer reading', newStatus: 'New status', reason: 'Reason', unassign: 'Unassign driver' }[field] ?? field);
const formatDate = (field: string) => field === 'startDate' || field === 'endDate' ? 'datetime-local' : ['scheduledDate', 'fuelDate'].includes(field) ? 'date' : 'text';
const getErrorMessage = (error: unknown, fallback: string) => isAxiosError<{ message?: string; title?: string }>(error)
  ? error.response?.data?.message ?? error.response?.data?.title ?? fallback
  : error instanceof Error ? error.message : fallback;

const AIAssistantPage: React.FC = () => {
  const navigate = useNavigate();
  const role = useSelector((state: { auth: { userRole: string } }) => state.auth.userRole);
  const [prompt, setPrompt] = useState('');
  const [plan, setPlan] = useState<AiActionPlan | null>(null);
  const [loading, setLoading] = useState(false);
  const [reasoningOpen, setReasoningOpen] = useState(false);
  const [error, setError] = useState('');
  const [vehicles, setVehicles] = useState<Vehicle[]>([]);
  const [drivers, setDrivers] = useState<Driver[]>([]);
  const [loadError, setLoadError] = useState('');
  const [values, setValues] = useState<Record<number, DraftValues>>({});
  const [states, setStates] = useState<Record<number, ActionState>>({});
  const permissions: Record<ActionDraft['action'], boolean> = {
    scheduleTrip: canCreateTrip(role), createMaintenance: canCreateMaintenance(role), logFuel: canCreateFuel(role),
    changeVehicleStatus: canChangeVehicleStatus(role), assignDriver: canAssignVehicleDriver(role), none: true,
  };

  useEffect(() => {
    void Promise.all([fetchVehicles(), fetchDrivers()]).then(([vehicleData, driverData]) => {
      setVehicles(vehicleData); setDrivers(driverData);
    }).catch(() => setLoadError('Fleet vehicles and drivers could not be loaded. Refresh before confirming actions.'));
  }, []);

  const submit = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!prompt.trim()) return;
    setLoading(true); setError(''); setPlan(null); setValues({}); setStates({});
    try {
      const fleetContext = JSON.stringify({
        vehicles: vehicles.map(({ vehicleNumber, manufacturer, model, status }) => ({ vehicleNumber, manufacturer, model, status })),
        drivers: drivers.map(({ firstName, lastName, email, status }) => ({ name: `${firstName} ${lastName}`, email, status })),
      });
      const result = await planAiAction({ message: prompt.trim(), context: fleetContext });
      setPlan(result);
      setValues(Object.fromEntries(result.actions.map((draft, index) => [index, draftData(draft)])));
      setStates(Object.fromEntries(result.actions.map((_, index) => [index, { status: 'Draft' }])));
    } catch (err) { setError(getErrorMessage(err, 'Unable to reach the AI assistant. Please try again later.')); }
    finally { setLoading(false); }
  };

  const confirm = async (draft: ActionDraft, index: number) => {
    if (!permissions[draft.action] || !plan) return;
    const data = values[index] ?? {};
    const vehicle = vehicles.find((item) => item.vehicleNumber.toLowerCase() === String(data.vehicleNumber ?? '').toLowerCase());
    const driver = drivers.find((item) => item.email.toLowerCase() === String(data.driverEmail ?? '').toLowerCase());
    const required = fieldsByAction[draft.action].filter((field) => !(field === 'description' || field === 'reason' || field === 'unassign' || (field === 'driverEmail' && data.unassign === 'true')) && !String(data[field] ?? '').trim());
    if (!vehicle || required.length || (['scheduleTrip', 'assignDriver'].includes(draft.action) && data.unassign !== 'true' && !driver)) {
      setStates((prev) => ({ ...prev, [index]: { status: 'Failed', error: !vehicle ? 'Choose a valid vehicle.' : required.length ? `Complete required fields: ${required.map(labelFor).join(', ')}.` : 'Choose a valid driver email.' } }));
      return;
    }
    const setState = (state: ActionState) => setStates((prev) => ({ ...prev, [index]: state }));
    setState({ status: 'Confirming' });
    try {
      if (draft.action === 'scheduleTrip') {
        if (new Date(String(data.startDate)) >= new Date(String(data.endDate))) throw new Error('Trip end time must be after its start time.');
        const result = await createTrip({ vehicleId: vehicle.id, driverId: driver!.id, source: String(data.source).trim(), destination: String(data.destination).trim(), startDate: new Date(String(data.startDate)).toISOString(), endDate: new Date(String(data.endDate)).toISOString() });
        setState({ status: 'Done', error: `Trip ${result.tripNumber} scheduled.` });
      } else if (draft.action === 'createMaintenance') {
        const cost = Number(data.cost); if (!Number.isFinite(cost) || cost < 0) throw new Error('Enter a non-negative maintenance cost.');
        await createMaintenanceRecord({ vehicleId: vehicle.id, serviceType: String(data.serviceType).trim(), description: String(data.description ?? '').trim(), scheduledDate: new Date(`${data.scheduledDate}T00:00:00`).toISOString(), cost, vendor: String(data.vendor).trim(), status: 'Scheduled' });
        setState({ status: 'Done', error: `Maintenance scheduled for ${vehicle.vehicleNumber}.` });
      } else if (draft.action === 'logFuel') {
        const quantity = Number(data.quantity), cost = Number(data.cost), odometerReading = Number(data.odometerReading);
        if ([quantity, cost, odometerReading].some((number) => !Number.isFinite(number) || number < 0)) throw new Error('Fuel quantity, cost, and odometer reading must be non-negative numbers.');
        await createFuelRecord({ vehicleId: vehicle.id, fuelDate: new Date(`${data.fuelDate}T00:00:00`).toISOString(), quantity, cost, odometerReading });
        setState({ status: 'Done', error: `Fuel log created for ${vehicle.vehicleNumber}.` });
      } else if (draft.action === 'changeVehicleStatus') {
        const status = String(data.newStatus);
        if (!['Active', 'InMaintenance', 'Decommissioned'].includes(status)) throw new Error('Choose Active, InMaintenance, or Decommissioned.');
        await changeVehicleStatus(vehicle.id, status, String(data.reason ?? '').trim());
        setState({ status: 'Done', error: `${vehicle.vehicleNumber} status changed to ${status}.` });
      } else if (draft.action === 'assignDriver') {
        await setVehicleAssignment(vehicle.id, data.unassign === 'true' ? null : driver!.id);
        setState({ status: 'Done', error: data.unassign === 'true' ? `Driver unassigned from ${vehicle.vehicleNumber}.` : `${driver!.email} assigned to ${vehicle.vehicleNumber}.` });
      }
    } catch (err) { setState({ status: 'Failed', error: getErrorMessage(err, `Unable to confirm ${actionLabels[draft.action].toLowerCase()}.`) }); }
  };

  const updateValue = (index: number, field: string, value: string) => setValues((prev) => ({ ...prev, [index]: { ...prev[index], [field]: value } }));
  return <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
    <PageHeader title="AI Assistant" subtitle="Prepare role-aware fleet action drafts for review." onBack={() => navigate(-1)} />
    <Card><CardContent sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
      <Typography variant="h3">Fleet copilot</Typography>
      <Typography variant="body2" color="text.secondary">Review each draft and confirm it individually. The assistant never submits actions without your confirmation.</Typography>
      <Paper component="form" onSubmit={submit} variant="outlined" sx={{ p: 2, display: 'flex', flexDirection: 'column', gap: 2 }}>
        <TextField label="Ask the AI assistant" value={prompt} onChange={(event) => setPrompt(event.target.value)} multiline minRows={2} />
        <Box sx={{ display: 'flex', gap: 1, flexWrap: 'wrap' }}>{prompts.map((item) => <Chip key={item} label={item} onClick={() => setPrompt(item)} clickable />)}</Box>
        <Button type="submit" variant="contained" disabled={loading || !prompt.trim()}>{loading ? 'Planning…' : 'Create drafts'}</Button>
      </Paper>
      {error && <Alert severity="error">{error}</Alert>}{loadError && <Alert severity="warning">{loadError}</Alert>}
      {plan && <>
        <Paper variant="outlined" sx={{ p: 2 }}><Typography variant="subtitle2">Assistant</Typography><Typography>{plan.assistantMessage}</Typography>
          {plan.reasoning && <><Button size="small" onClick={() => setReasoningOpen((open) => !open)}>{reasoningOpen ? 'Hide reasoning' : 'Show reasoning'}</Button><Collapse in={reasoningOpen}><Typography variant="body2" color="text.secondary">{plan.reasoning}</Typography></Collapse></>}
        </Paper>
        {plan.actions.map((draft, index) => {
          const permitted = permissions[draft.action]; const state = states[index] ?? { status: 'Draft' as const };
          return <Card variant="outlined" key={`${draft.action}-${index}`}><CardContent sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', gap: 1, alignItems: 'center' }}><Typography variant="h3">Review {actionLabels[draft.action]} draft</Typography><Chip label={state.status} color={state.status === 'Done' ? 'success' : state.status === 'Failed' ? 'error' : 'primary'} /></Box>
            {!permitted && <Alert severity="warning">Your role cannot confirm this action. Ask an authorized fleet user.</Alert>}
            {state.error && <Alert severity={state.status === 'Failed' ? 'error' : 'success'}>{state.error}</Alert>}
            {draft.action === 'none' ? <Typography>{plan.assistantMessage}</Typography> : <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', md: '1fr 1fr' }, gap: 2 }}>
              {fieldsByAction[draft.action].map((field) => {
                const value = values[index]?.[field] ?? '';
                if (field === 'vehicleNumber') return <TextField key={field} select label={labelFor(field)} value={value} onChange={(event) => updateValue(index, field, event.target.value)}>{vehicles.map((item) => <MenuItem key={item.id} value={item.vehicleNumber}>{item.vehicleNumber} · {item.manufacturer} {item.model}</MenuItem>)}</TextField>;
                if (field === 'driverEmail') return <TextField key={field} select label={labelFor(field)} value={value} disabled={values[index]?.unassign === 'true'} onChange={(event) => updateValue(index, field, event.target.value)}><MenuItem value="">Choose a driver</MenuItem>{drivers.map((item) => <MenuItem key={item.id} value={item.email}>{item.firstName} {item.lastName} · {item.email}</MenuItem>)}</TextField>;
                if (field === 'unassign') return <TextField key={field} select label={labelFor(field)} value={value} onChange={(event) => updateValue(index, field, event.target.value)}><MenuItem value="false">Assign driver</MenuItem><MenuItem value="true">Unassign current driver</MenuItem></TextField>;
                if (field === 'newStatus') return <TextField key={field} select label={labelFor(field)} value={value} onChange={(event) => updateValue(index, field, event.target.value)}>{['Active', 'InMaintenance', 'Decommissioned'].map((item) => <MenuItem key={item} value={item}>{item}</MenuItem>)}</TextField>;
                return <TextField key={field} label={labelFor(field)} type={formatDate(field)} value={value} onChange={(event) => updateValue(index, field, event.target.value)} multiline={field === 'description' || field === 'reason'} slotProps={{ inputLabel: { shrink: ['startDate', 'endDate', 'scheduledDate', 'fuelDate'].includes(field) ? true : undefined } }} />;
              })}
            </Box>}
            {draft.action !== 'none' && <Button variant="contained" disabled={!permitted || state.status === 'Confirming' || state.status === 'Done' || Boolean(loadError)} onClick={() => void confirm(draft, index)}>{state.status === 'Confirming' ? 'Confirming…' : state.status === 'Done' ? 'Confirmed' : `Confirm ${actionLabels[draft.action].toLowerCase()}`}</Button>}
          </CardContent></Card>;
        })}
      </>}
    </CardContent></Card>
  </Box>;
};

export default AIAssistantPage;
