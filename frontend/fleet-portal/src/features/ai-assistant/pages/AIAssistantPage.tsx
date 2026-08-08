import React from 'react';
import {
  Box,
  Card,
  CardContent,
  Chip,
  Paper,
  Typography,
} from '@mui/material';
import {
  AutoAwesome,
  Route as RouteIcon,
  TrendingUp,
  WarningAmber,
} from '@mui/icons-material';
import PageHeader from '../../../components/PageHeader';

const insightCards = [
  {
    title: 'Incident triage',
    description: 'Surface high-risk vehicles and overdue maintenance before they impact operations.',
    icon: <WarningAmber color="error" />,
  },
  {
    title: 'Route optimisation',
    description: 'Recommend efficient dispatch changes based on live fleet density and service windows.',
    icon: <RouteIcon color="primary" />,
  },
  {
    title: 'Consumption insights',
    description: 'Identify fuel anomalies and suggest corrective actions for underperforming units.',
    icon: <TrendingUp color="success" />,
  },
];

const prompts = [
  'Summarise urgent maintenance issues for the next 24 hours.',
  'Which vehicles are likely to exceed fuel budgets this month?',
  'Suggest the best dispatch plan for today’s active routes.',
];

const AIAssistantPage: React.FC = () => (
  <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
    <PageHeader
      title="AI Assistant"
      subtitle="Ask for operational summaries, recommendations, and fleet risk alerts."
    />

    <Card>
      <CardContent sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
        <Box sx={{ display: 'flex', flexDirection: { xs: 'column', md: 'row' }, gap: 2, alignItems: { xs: 'flex-start', md: 'center' }, justifyContent: 'space-between' }}>
          <Box>
            <Typography variant="h3">Fleet copilot</Typography>
            <Typography variant="body2" color="text.secondary" sx={{ mt: 0.5 }}>
              Use conversational prompts to accelerate planning and uncover operational issues.
            </Typography>
          </Box>
          <Chip icon={<AutoAwesome />} label="Live insights ready" color="primary" />
        </Box>

        <Paper variant="outlined" sx={{ p: 2, borderRadius: 2, bgcolor: '#f8fafc' }}>
          <Typography variant="subtitle2" sx={{ mb: 1 }}>
            Try one of these prompts
          </Typography>
          <Box sx={{ display: 'flex', gap: 1, flexWrap: 'wrap' }}>
            {prompts.map((prompt) => (
              <Chip key={prompt} label={prompt} variant="outlined" />
            ))}
          </Box>
        </Paper>
      </CardContent>
    </Card>

    <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', md: 'repeat(3, 1fr)' }, gap: 2 }}>
      {insightCards.map((card) => (
        <Card key={card.title}>
          <CardContent>
            <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1.5 }}>
              {card.icon}
              <Typography variant="h3">{card.title}</Typography>
            </Box>
            <Typography variant="body2" color="text.secondary">
              {card.description}
            </Typography>
          </CardContent>
        </Card>
      ))}
    </Box>
  </Box>
);

export default AIAssistantPage;
