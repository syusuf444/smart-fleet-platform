import React from 'react';
import { Box, Card, CardContent, Chip, Typography } from '@mui/material';
import { LocalGasStation, TrendingDown, AttachMoney } from '@mui/icons-material';
import PageHeader from '../../../components/PageHeader';

const fuelCards = [
  { title: 'Efficiency outliers', detail: '2 vehicles are consuming above expected rates and may need driver coaching.', icon: <TrendingDown color="warning" /> },
  { title: 'Spend trend', detail: 'This month’s average fuel cost is tracking 6% above budget.', icon: <AttachMoney color="primary" /> },
  { title: 'Refuel activity', detail: 'Daily refuelling updates are flowing in from 83% of active vehicles.', icon: <LocalGasStation color="success" /> },
];

const FuelDashboardPage: React.FC = () => (
  <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
    <PageHeader title="Fuel Dashboard" subtitle="Monitor efficiency, highlight anomalies, and contain rising fuel costs." />

    <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', md: 'repeat(3, 1fr)' }, gap: 2 }}>
      {fuelCards.map((item) => (
        <Card key={item.title}>
          <CardContent>
            <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
              {item.icon}
              <Typography variant="h6">{item.title}</Typography>
            </Box>
            <Typography variant="body2" color="text.secondary" sx={{ mb: 1.5 }}>
              {item.detail}
            </Typography>
            <Chip label="Needs attention" color="warning" />
          </CardContent>
        </Card>
      ))}
    </Box>
  </Box>
);

export default FuelDashboardPage;
