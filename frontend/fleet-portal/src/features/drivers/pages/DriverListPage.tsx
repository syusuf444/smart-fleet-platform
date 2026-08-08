import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Box, Card, CardContent, CircularProgress, Typography } from '@mui/material';
import { fetchDrivers } from '../../../api/fleetApi';
import type { Driver } from '../../../api/types';

const DriverListPage: React.FC = () => {
  const navigate = useNavigate();
  const [drivers, setDrivers] = useState<Driver[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const loadDrivers = async () => {
      try {
        const data = await fetchDrivers();
        setDrivers(data);
      } catch (err) {
        setError('Unable to load drivers at this time.');
      } finally {
        setLoading(false);
      }
    };

    loadDrivers();
  }, []);

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
      <Typography variant="h5">Driver Roster</Typography>
      {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', py: 6 }}>
          <CircularProgress />
        </Box>
      ) : error ? (
        <Typography color="error">{error}</Typography>
      ) : (
        <Box sx={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(240px, 1fr))', gap: 24 }}>
          {drivers.map((driver) => (
            <Card
              key={driver.id}
              onClick={() => navigate(`/drivers/${driver.id}`)}
              sx={{
                borderRadius: 3,
                cursor: 'pointer',
                transition: 'transform 0.2s ease, box-shadow 0.2s ease',
                '&:hover': {
                  transform: 'translateY(-4px)',
                  boxShadow: 6,
                },
              }}
            >
              <CardContent>
                <Typography variant="h6">{driver.firstName} {driver.lastName}</Typography>
                <Typography variant="body2" color="text.secondary">
                  {driver.email}
                </Typography>
                <Typography variant="subtitle2" sx={{ mt: 1 }}>
                  Status: {driver.status}
                </Typography>
              </CardContent>
            </Card>
          ))}
        </Box>
      )}
    </Box>
  );
};

export default DriverListPage;
