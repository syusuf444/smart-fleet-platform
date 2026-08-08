import { createTheme } from '@mui/material/styles';

export const theme = createTheme({
  palette: {
    // Primary brand colors
    primary: {
      main: '#2563eb',
      light: '#3b82f6',
      dark: '#1d4ed8',
      contrastText: '#ffffff',
    },
    secondary: {
      main: '#565e74',
      light: '#64748b',
      dark: '#334155',
      contrastText: '#ffffff',
    },
    // Surface colors per design system
    background: {
      default: '#faf8ff',
      paper: '#ffffff',
    },
    text: {
      primary: '#191b23',
      secondary: '#434655',
    },
    // Status/semantic colors
    success: {
      main: '#059669',
      light: '#10b981',
      dark: '#047857',
      contrastText: '#ffffff',
    },
    warning: {
      main: '#d97706',
      light: '#f59e0b',
      dark: '#b45309',
      contrastText: '#ffffff',
    },
    error: {
      main: '#ba1a1a',
      light: '#dc2626',
      dark: '#991b1b',
      contrastText: '#ffffff',
    },
    info: {
      main: '#0284c7',
      light: '#0ea5e9',
      dark: '#075985',
      contrastText: '#ffffff',
    },
    action: {
      active: '#2563eb',
      hover: '#f1f5f9',
      selected: '#e2e8f0',
      disabled: '#cbd5e1',
      focus: '#e0e7ff',
    },
    divider: '#e2e8f0',
  },
  typography: {
    fontFamily: ['Inter', 'sans-serif'].join(','),
    h1: {
      fontSize: '30px',
      fontWeight: 700,
      lineHeight: '38px',
      letterSpacing: '-0.02em',
    },
    h2: {
      fontSize: '20px',
      fontWeight: 600,
      lineHeight: '28px',
    },
    h3: {
      fontSize: '16px',
      fontWeight: 600,
      lineHeight: '24px',
    },
    h4: {
      fontSize: '16px',
      fontWeight: 600,
      lineHeight: '24px',
    },
    h5: {
      fontSize: '14px',
      fontWeight: 600,
      lineHeight: '20px',
    },
    h6: {
      fontSize: '14px',
      fontWeight: 600,
      lineHeight: '20px',
    },
    body1: {
      fontSize: '16px',
      fontWeight: 400,
      lineHeight: '24px',
    },
    body2: {
      fontSize: '14px',
      fontWeight: 400,
      lineHeight: '20px',
    },
    caption: {
      fontSize: '13px',
      fontWeight: 400,
      lineHeight: '18px',
    },
    overline: {
      fontSize: '12px',
      fontWeight: 600,
      lineHeight: '16px',
      letterSpacing: '0.05em',
      textTransform: 'uppercase',
    },
    button: {
      textTransform: 'none',
      fontSize: '14px',
      fontWeight: 600,
      lineHeight: '20px',
    },
  },
  shape: {
    borderRadius: 4,
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: '4px',
          textTransform: 'none',
          fontWeight: 600,
          fontSize: '14px',
          padding: '10px 16px',
        },
        contained: {
          boxShadow: 'none',
          '&:hover': {
            boxShadow: '0 2px 4px rgba(15, 23, 42, 0.12)',
          },
        },
        outlined: {
          borderWidth: '1px',
          borderColor: '#cbd5e1',
          '&:hover': {
            borderColor: '#94a3b8',
            backgroundColor: 'rgba(100, 116, 139, 0.04)',
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: '4px',
          backgroundColor: '#ffffff',
          border: '1px solid #e2e8f0',
          boxShadow: '0 1px 3px rgba(15, 23, 42, 0.08)',
          '&:hover': {
            boxShadow: '0 2px 6px rgba(15, 23, 42, 0.12)',
          },
        },
      },
    },
    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            borderRadius: '4px',
            '& fieldset': {
              borderColor: '#cbd5e1',
            },
            '&:hover fieldset': {
              borderColor: '#94a3b8',
            },
            '&.Mui-focused fieldset': {
              borderColor: '#2563eb',
              borderWidth: '1px',
            },
          },
        },
      },
    },
    MuiTable: {
      styleOverrides: {
        root: {
          borderCollapse: 'collapse',
        },
      },
    },
    MuiTableHead: {
      styleOverrides: {
        root: {
          backgroundColor: '#f1f5f9',
        },
      },
    },
    MuiTableCell: {
      styleOverrides: {
        root: {
          fontSize: '13px',
          borderColor: '#e2e8f0',
          padding: '12px 16px',
        },
        head: {
          fontWeight: 600,
          fontSize: '12px',
          textTransform: 'uppercase',
          color: '#191b23',
        },
      },
    },
    MuiTableRow: {
      styleOverrides: {
        root: {
          minHeight: '40px',
          '&:hover': {
            backgroundColor: '#f8fafc',
          },
        },
      },
    },
    MuiChip: {
      styleOverrides: {
        root: {
          borderRadius: '8px',
          fontSize: '12px',
          fontWeight: 600,
        },
      },
    },
    MuiPaper: {
      styleOverrides: {
        root: {
          backgroundImage: 'none',
        },
      },
    },
  },
});
