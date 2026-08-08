import React from 'react';
import { Card, CardContent, Typography, Box, IconButton } from '@mui/material';
import { ChevronRight as ChevronRightIcon } from '@mui/icons-material';

interface QuickActionCardProps {
  icon: React.ReactNode;
  title: string;
  description?: string;
  onClick?: () => void;
  color?: 'primary' | 'success' | 'warning' | 'error' | 'info';
}

const colorMap: Record<string, string> = {
  primary: 'rgba(37, 99, 235, 0.08)',
  success: 'rgba(5, 150, 105, 0.08)',
  warning: 'rgba(217, 119, 6, 0.08)',
  error: 'rgba(220, 38, 38, 0.08)',
  info: 'rgba(2, 132, 199, 0.08)',
};

const iconColorMap: Record<string, string> = {
  primary: '#2563eb',
  success: '#059669',
  warning: '#d97706',
  error: '#dc2626',
  info: '#0284c7',
};

const QuickActionCard: React.FC<QuickActionCardProps> = ({
  icon,
  title,
  description,
  onClick,
  color = 'primary',
}) => {
  return (
    <Card
      onClick={onClick}
      sx={{
        borderRadius: '4px',
        backgroundColor: '#ffffff',
        border: '1px solid #e2e8f0',
        boxShadow: '0 1px 3px rgba(15, 23, 42, 0.08)',
        transition: 'all 0.2s ease',
        cursor: onClick ? 'pointer' : 'default',
        '&:hover': {
          boxShadow: onClick ? '0 2px 6px rgba(15, 23, 42, 0.12)' : '0 1px 3px rgba(15, 23, 42, 0.08)',
          transform: onClick ? 'translateY(-2px)' : 'none',
        },
      }}
    >
      <CardContent sx={{ p: 3, display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
        <Box sx={{ display: 'flex', gap: 2, flex: 1 }}>
          <Box
            sx={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              width: 48,
              height: 48,
              borderRadius: '8px',
              backgroundColor: colorMap[color],
              color: iconColorMap[color],
            }}
          >
            {icon}
          </Box>
          <Box sx={{ flex: 1 }}>
            <Typography
              variant="h6"
              sx={{ fontSize: '16px', fontWeight: 600, color: '#191b23', mb: 0.5 }}
            >
              {title}
            </Typography>
            {description && (
              <Typography variant="body2" sx={{ fontSize: '14px', color: '#5c647a' }}>
                {description}
              </Typography>
            )}
          </Box>
        </Box>

        {onClick && (
          <IconButton
            size="small"
            sx={{
              color: 'text.secondary',
              ml: 1,
              '&:hover': { backgroundColor: 'rgba(37, 99, 235, 0.04)' },
            }}
          >
            <ChevronRightIcon />
          </IconButton>
        )}
      </CardContent>
    </Card>
  );
};

export default QuickActionCard;
