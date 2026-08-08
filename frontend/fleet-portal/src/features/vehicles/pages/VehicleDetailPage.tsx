import React, { useEffect, useState } from 'react';
import {
  Box,
  Card,
  CardContent,
  Chip,
  CircularProgress,
  Divider,
  Typography,
} from '@mui/material';
import {
  Build,
  LocalGasStation,
} from '@mui/icons-material';
import { useParams } from 'react-router-dom';
import { fetchVehicleById } from '../../../api/fleetApi';
import type { Vehicle } from '../../../api/types';

const VehicleDetailPage: React.FC = () => {
  const { id } = useParams();
  const [vehicle, setVehicle] = useState<Vehicle | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const loadVehicle = async () => {
      if (!id) return;

      try {
        const data = await fetchVehicleById(id);
        setVehicle(data ?? null);
      } catch (err) {
        setError('Unable to load vehicle details.');
      } finally {
        setLoading(false);
      }
    };

    loadVehicle();
  }, [id]);

  return (
    <Box>
      <Typography variant="h5" gutterBottom>
        Vehicle Details
      </Typography>
      {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', py: 6 }}>
          <CircularProgress />
        </Box>
      ) : error ? (
        <Typography color="error">{error}</Typography>
      ) : vehicle ? (
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <Card sx={{ borderRadius: 3 }}>
            <CardContent>
              <Box sx={{ display: 'flex', flexDirection: { xs: 'column', md: 'row' }, justifyContent: 'space-between', gap: 2 }}>
                <Box>
                  <Typography variant="h6">{vehicle.vehicleNumber}</Typography>
                  <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
                    {vehicle.manufacturer} {vehicle.model} • {vehicle.year}
                  </Typography>
                </Box>
                <Chip label={vehicle.status} color={vehicle.status.toLowerCase() === 'active' ? 'success' : 'default'} />
              </Box>
              <Divider sx={{ my: 2 }} />
              <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', sm: 'repeat(3, 1fr)' }, gap: 2 }}>
                <Box sx={{ p: 1.5, border: '1px solid #e2e8f0', borderRadius: 2 }}>
                  <Typography variant="caption" color="text.secondary">Fuel capacity</Typography>
                  <Typography variant="h6" sx={{ mt: 0.5 }}>{vehicle.fuelCapacity} L</Typography>
                </Box>
                <Box sx={{ p: 1.5, border: '1px solid #e2e8f0', borderRadius: 2 }}>
                  <Typography variant="caption" color="text.secondary">Operating status</Typography>
                  <Typography variant="h6" sx={{ mt: 0.5 }}>{vehicle.status}</Typography>
                </Box>
                <Box sx={{ p: 1.5, border: '1px solid #e2e8f0', borderRadius: 2 }}>
                  <Typography variant="caption" color="text.secondary">Last updated</Typography>
                  <Typography variant="h6" sx={{ mt: 0.5 }}>{new Date(vehicle.createdAt).toLocaleDateString()}</Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>

          <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', md: 'repeat(2, 1fr)' }, gap: 2 }}>
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                  <LocalGasStation color="primary" />
                  <Typography variant="h6">Fuel efficiency</Typography>
                </Box>
                <Typography variant="body2" color="text.secondary">
                  Consumption trends and refill intervals will surface here as sensor and telematics data is connected.
                </Typography>
              </CardContent>
            </Card>
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                  <Build color="primary" />
                  <Typography variant="h6">Maintenance readiness</Typography>
                </Box>
                <Typography variant="body2" color="text.secondary">
                  Scheduled service milestones and work order history can be tracked directly from this panel.
                </Typography>
              </CardContent>
            </Card>
          </Box>
        </Box>
      ) : (
        <Typography>No vehicle found.</Typography>
      )}
    </Box>
  );
};

export default VehicleDetailPage;
