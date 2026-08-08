import React from 'react';
import { Card, CardContent, Typography, Box, LinearProgress } from '@mui/material';

interface KPIWidgetProps {
  title: string;
  value: string | number;
  unit?: string;
  subtitle?: string;
  progress?: number;
  trend?: { value: number; label: string; isPositive: boolean };
  icon?: React.ReactNode;
}

const KPIWidget: React.FC<KPIWidgetProps> = ({
  title,
  value,
  unit,
  subtitle,
  progress,
  trend,
  icon,
}) => {
  return (
    <Card
      sx={{
        borderRadius: '4px',
        backgroundColor: '#ffffff',
        border: '1px solid #e2e8f0',
        boxShadow: '0 1px 3px rgba(15, 23, 42, 0.08)',
        transition: 'all 0.2s ease',
        '&:hover': {
          boxShadow: '0 2px 6px rgba(15, 23, 42, 0.12)',
        },
      }}
    >
      <CardContent sx={{ p: 3 }}>
        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
          <Box sx={{ flex: 1 }}>
            <Typography
              variant="overline"
              sx={{ fontSize: '12px', fontWeight: 600, color: '#434655', mb: 1 }}
            >
              {title}
            </Typography>
            <Box sx={{ display: 'flex', alignItems: 'baseline', gap: 1, mb: 1 }}>
              <Typography
                variant="h2"
                sx={{ fontSize: '32px', fontWeight: 700, lineHeight: '40px', color: '#191b23' }}
              >
                {value}
              </Typography>
              {unit && (
                <Typography sx={{ fontSize: '14px', color: '#434655', fontWeight: 500 }}>
                  {unit}
                </Typography>
              )}
            </Box>

            {subtitle && (
              <Typography variant="caption" sx={{ color: '#5c647a', display: 'block', mb: 2 }}>
                {subtitle}
              </Typography>
            )}

            {progress !== undefined && (
              <Box sx={{ mb: 2 }}>
                <LinearProgress
                  variant="determinate"
                  value={progress}
                  sx={{
                    height: 4,
                    borderRadius: '2px',
                    backgroundColor: '#e2e8f0',
                    '& .MuiLinearProgress-bar': {
                      borderRadius: '2px',
                      backgroundColor: '#2563eb',
                    },
                  }}
                />
              </Box>
            )}

            {trend && (
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 0.5 }}>
                <Typography
                  sx={{
                    fontSize: '13px',
                    fontWeight: 600,
                    color: trend.isPositive ? '#059669' : '#dc2626',
                  }}
                >
                  {trend.isPositive ? '↑' : '↓'} {trend.value}%
                </Typography>
                <Typography sx={{ fontSize: '12px', color: '#5c647a' }}>
                  {trend.label}
                </Typography>
              </Box>
            )}
          </Box>

          {icon && (
            <Box
              sx={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                width: 48,
                height: 48,
                borderRadius: '8px',
                backgroundColor: 'rgba(37, 99, 235, 0.08)',
                color: 'primary.main',
                ml: 2,
              }}
            >
              {icon}
            </Box>
          )}
        </Box>
      </CardContent>
    </Card>
  );
};

export default KPIWidget;
