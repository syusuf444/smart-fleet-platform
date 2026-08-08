import React from 'react';
import { Box, Card, CardContent, Chip, Typography } from '@mui/material';
import { Build, CalendarToday, WarningAmber } from '@mui/icons-material';
import PageHeader from '../../../components/PageHeader';

const maintenanceItems = [
  { title: 'Upcoming inspections', detail: '4 vehicles require brake and oil checks in the next 72 hours.', status: 'High priority' },
  { title: 'Open work orders', detail: '3 service requests currently awaiting parts and technician assignment.', status: 'In progress' },
  { title: 'Compliance window', detail: '2 drivers have licence or medical checks due this week.', status: 'Watchlist' },
];

const MaintenanceDashboardPage: React.FC = () => (
  <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
    <PageHeader title="Maintenance Dashboard" subtitle="Coordinate service readiness and avoid disruptions across the fleet." />

    <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', md: 'repeat(3, 1fr)' }, gap: 2 }}>
      {maintenanceItems.map((item) => (
        <Card key={item.title}>
          <CardContent>
            <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
              {item.title.includes('inspection') ? <CalendarToday color="primary" /> : <Build color="primary" />}
              <Typography variant="h6">{item.title}</Typography>
            </Box>
            <Typography variant="body2" color="text.secondary" sx={{ mb: 1.5 }}>
              {item.detail}
            </Typography>
            <Chip label={item.status} color={item.status === 'High priority' ? 'error' : 'default'} />
          </CardContent>
        </Card>
      ))}
    </Box>

    <Card>
      <CardContent>
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
          <WarningAmber color="warning" />
          <Typography variant="h6">Fleet readiness summary</Typography>
        </Box>
        <Typography variant="body2" color="text.secondary">
          Keep service windows aligned with dispatch planning to prevent avoidable downtime and improve utilisation.
        </Typography>
      </CardContent>
    </Card>
  </Box>
);

export default MaintenanceDashboardPage;
