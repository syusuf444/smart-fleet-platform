import React from 'react';
import { Box, Button, Typography } from '@mui/material';

interface EmptyStateProps {
  title: string;
  description?: string;
  actionLabel?: string;
  onAction?: () => void;
}

const EmptyState: React.FC<EmptyStateProps> = ({
  title,
  description,
  actionLabel,
  onAction,
}) => (
  <Box
    sx={{
      border: '1px dashed #cbd5e1',
      borderRadius: '4px',
      bgcolor: '#ffffff',
      py: 6,
      px: 3,
      textAlign: 'center',
    }}
  >
    <Typography variant="h6" sx={{ mb: 1 }}>
      {title}
    </Typography>
    {description && (
      <Typography variant="body2" sx={{ color: '#434655', mb: actionLabel ? 2 : 0 }}>
        {description}
      </Typography>
    )}
    {actionLabel && onAction && (
      <Button variant="contained" onClick={onAction}>
        {actionLabel}
      </Button>
    )}
  </Box>
);

export default EmptyState;
