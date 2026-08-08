import React from 'react';
import { Box, Skeleton } from '@mui/material';

interface SkeletonLoaderProps {
  rows?: number;
}

const SkeletonLoader: React.FC<SkeletonLoaderProps> = ({ rows = 4 }) => (
  <Box sx={{ display: 'grid', gap: 2 }}>
    {Array.from({ length: rows }).map((_, index) => (
      <Skeleton key={index} variant="rounded" height={72} />
    ))}
  </Box>
);

export default SkeletonLoader;
