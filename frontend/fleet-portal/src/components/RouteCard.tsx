import React from 'react';
import { Card, CardContent, Box, Typography } from '@mui/material';

interface RouteCardProps {
  status: string;
  pickupTime: string;
  pickupLocation: string;
  dropoffTime: string;
  dropoffLocation: string;
  onStartTrip?: () => void;
  onStop?: () => void;
  mapComponent?: React.ReactNode;
}

const RouteCard: React.FC<RouteCardProps> = ({
  status,
  pickupTime,
  pickupLocation,
  dropoffTime,
  dropoffLocation,
  mapComponent,
}) => {
  return (
    <Card
      sx={{
        borderRadius: '4px',
        overflow: 'hidden',
        border: '1px solid #e2e8f0',
        boxShadow: '0 1px 3px rgba(15, 23, 42, 0.08)',
        backgroundColor: '#ffffff',
      }}
    >
      {mapComponent && (
        <Box sx={{ position: 'relative', height: 240, backgroundColor: '#e0e7ff' }}>
          {mapComponent}
        </Box>
      )}

      <CardContent sx={{ p: 3 }}>
        <Box sx={{ mb: 2 }}>
          <Box
            sx={{
              display: 'inline-block',
              px: 2,
              py: 0.5,
              backgroundColor: 'rgba(37, 99, 235, 0.12)',
              borderRadius: '4px',
            }}
          >
            <Typography
              variant="overline"
              sx={{ fontSize: '12px', fontWeight: 600, color: '#2563eb' }}
            >
              {status}
            </Typography>
          </Box>
        </Box>

        <Box sx={{ mb: 3 }}>
          <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
            <Box sx={{ width: 4, borderRadius: '2px', backgroundColor: '#2563eb' }} />
            <Box sx={{ flex: 1 }}>
              <Typography
                variant="overline"
                sx={{ fontSize: '12px', fontWeight: 600, color: '#5c647a', display: 'block', mb: 0.5 }}
              >
                Pickup ({pickupTime})
              </Typography>
              <Typography sx={{ fontSize: '14px', color: '#191b23', fontWeight: 500 }}>
                {pickupLocation}
              </Typography>
            </Box>
          </Box>

          <Box sx={{ display: 'flex', gap: 2 }}>
            <Box sx={{ width: 4, borderRadius: '2px', backgroundColor: '#10b981' }} />
            <Box sx={{ flex: 1 }}>
              <Typography
                variant="overline"
                sx={{ fontSize: '12px', fontWeight: 600, color: '#5c647a', display: 'block', mb: 0.5 }}
              >
                Dropoff (Est. {dropoffTime})
              </Typography>
              <Typography sx={{ fontSize: '14px', color: '#191b23', fontWeight: 500 }}>
                {dropoffLocation}
              </Typography>
            </Box>
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
};

export default RouteCard;
