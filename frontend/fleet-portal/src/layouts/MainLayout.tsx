import React, { useState } from 'react';
import {
  Box,
  AppBar,
  Drawer,
  Toolbar,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Typography,
  useMediaQuery,
  useTheme,
  Avatar,
  Menu,
  MenuItem,
  Badge,
  IconButton,
  Container,
  InputBase,
  Chip,
} from '@mui/material';
import { useNavigate, useLocation } from 'react-router-dom';
import {
  Dashboard as DashboardIcon,
  DirectionsCar as VehiclesIcon,
  People as DriversIcon,
  LocalGasStation as FuelIcon,
  Build as MaintenanceIcon,
  Route as TripsIcon,
  Analytics as AnalyticsIcon,
  SmartToy as AiIcon,
  Settings as SettingsIcon,
  Notifications as NotificationsIcon,
  Person as ProfileIcon,
  Logout as LogoutIcon,
  Menu as MenuIcon,
  Close as CloseIcon,
  Search as SearchIcon,
} from '@mui/icons-material';

interface NavItem {
  label: string;
  path: string;
  icon: React.ReactNode;
  disabled?: boolean;
}

const navItems: NavItem[] = [
  { label: 'Dashboard', path: '/dashboard', icon: <DashboardIcon /> },
  { label: 'Vehicles', path: '/vehicles', icon: <VehiclesIcon /> },
  { label: 'Drivers', path: '/drivers', icon: <DriversIcon /> },
  { label: 'Trips', path: '/trips', icon: <TripsIcon />, disabled: true },
  { label: 'Maintenance', path: '/maintenance', icon: <MaintenanceIcon /> },
  { label: 'Fuel', path: '/fuel', icon: <FuelIcon /> },
  { label: 'Analytics', path: '/analytics', icon: <AnalyticsIcon />, disabled: true },
  { label: 'AI Assistant', path: '/ai-assistant', icon: <AiIcon /> },
  { label: 'Settings', path: '/settings', icon: <SettingsIcon />, disabled: true },
];

interface MainLayoutProps {
  children: React.ReactNode;
  userName?: string;
  userInitials?: string;
}

const sidebarBackground = '#0f172a';
const sidebarText = '#cbd5e1';
const sidebarActiveText = '#ffffff';

const MainLayout: React.FC<MainLayoutProps> = ({
  children,
  userName = 'Fleet Manager',
  userInitials = 'FM',
}) => {
  const navigate = useNavigate();
  const location = useLocation();
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  const handleMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    localStorage.removeItem('authToken');
    localStorage.removeItem('authUserName');
    handleMenuClose();
    navigate('/login');
  };

  const isActive = (path: string) => location.pathname === path;

  const drawerContent = (
    <Box sx={{ display: 'flex', flexDirection: 'column', height: '100%', bgcolor: sidebarBackground }}>
      <Box sx={{ px: 2.5, py: 3, borderBottom: '1px solid rgba(148, 163, 184, 0.15)' }}>
        <Typography variant="h6" sx={{ fontWeight: 700, color: sidebarActiveText }}>
          Smart Fleet
        </Typography>
        <Typography variant="caption" sx={{ color: sidebarText }}>
          Intelligence Platform
        </Typography>
      </Box>

      <List sx={{ flex: 1, px: 1.5, py: 2 }}>
        {navItems.map((item) => (
          <ListItem key={item.path} disablePadding sx={{ mb: 0.5 }}>
            <ListItemButton
              disabled={item.disabled}
              onClick={() => {
                if (!item.disabled) {
                  navigate(item.path);
                  setSidebarOpen(false);
                }
              }}
              selected={isActive(item.path)}
              sx={{
                borderRadius: '4px',
                minHeight: 44,
                color: isActive(item.path) ? sidebarActiveText : sidebarText,
                backgroundColor: isActive(item.path) ? 'rgba(37, 99, 235, 0.18)' : 'transparent',
                borderLeft: isActive(item.path) ? '4px solid #2563eb' : '4px solid transparent',
                pl: isActive(item.path) ? '12px' : '16px',
                '&.Mui-selected': {
                  backgroundColor: 'rgba(37, 99, 235, 0.18)',
                },
                '&.Mui-selected:hover': {
                  backgroundColor: 'rgba(37, 99, 235, 0.24)',
                },
                '&:hover': {
                  backgroundColor: 'rgba(148, 163, 184, 0.12)',
                },
                '&.Mui-disabled': {
                  opacity: 0.45,
                },
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 40,
                  color: isActive(item.path) ? '#60a5fa' : sidebarText,
                }}
              >
                {item.icon}
              </ListItemIcon>
              <ListItemText
                primary={item.label}
                slotProps={{
                  primary: {
                    sx: { fontSize: '14px', fontWeight: isActive(item.path) ? 600 : 500 },
                  },
                }}
              />
              {item.disabled && (
                <Chip label="Soon" size="small" sx={{ height: 20, fontSize: '10px', bgcolor: 'rgba(148, 163, 184, 0.15)', color: sidebarText }} />
              )}
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Box>
  );

  return (
    <Box sx={{ display: 'flex', bgcolor: '#f8fafc', minHeight: '100vh' }}>
      {!isMobile && (
        <Drawer
          variant="permanent"
          sx={{
            width: 260,
            flexShrink: 0,
            '& .MuiDrawer-paper': {
              width: 260,
              boxSizing: 'border-box',
              bgcolor: sidebarBackground,
              border: 'none',
            },
          }}
        >
          {drawerContent}
        </Drawer>
      )}

      {isMobile && (
        <Drawer
          anchor="left"
          open={sidebarOpen}
          onClose={() => setSidebarOpen(false)}
          sx={{
            '& .MuiDrawer-paper': {
              bgcolor: sidebarBackground,
              border: 'none',
            },
          }}
        >
          {drawerContent}
        </Drawer>
      )}

      <Box sx={{ flex: 1, display: 'flex', flexDirection: 'column', minWidth: 0 }}>
        <AppBar
          position="sticky"
          sx={{
            backgroundColor: '#ffffff',
            color: 'text.primary',
            boxShadow: '0 1px 3px rgba(15, 23, 42, 0.08)',
            borderBottom: '1px solid #e2e8f0',
            zIndex: 10,
          }}
        >
          <Toolbar sx={{ display: 'flex', justifyContent: 'space-between', gap: 2, px: { xs: 2, md: 3 }, minHeight: 64 }}>
            <Box sx={{ display: 'flex', alignItems: 'center', gap: 2, flex: 1 }}>
              {isMobile && (
                <IconButton edge="start" color="inherit" aria-label="menu" onClick={() => setSidebarOpen(!sidebarOpen)}>
                  {sidebarOpen ? <CloseIcon /> : <MenuIcon />}
                </IconButton>
              )}
              <Box
                sx={{
                  display: { xs: 'none', md: 'flex' },
                  alignItems: 'center',
                  gap: 1,
                  px: 1.5,
                  py: 0.75,
                  border: '1px solid #e2e8f0',
                  borderRadius: '4px',
                  bgcolor: '#f8fafc',
                  maxWidth: 420,
                  flex: 1,
                }}
              >
                <SearchIcon sx={{ color: '#64748b', fontSize: 20 }} />
                <InputBase placeholder="Search fleet, drivers, vehicles..." sx={{ flex: 1, fontSize: '14px' }} />
              </Box>
            </Box>

            <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
              <IconButton sx={{ color: 'text.secondary' }}>
                <Badge badgeContent={2} color="error">
                  <NotificationsIcon />
                </Badge>
              </IconButton>

              <IconButton onClick={handleMenuOpen} sx={{ p: 0.5 }}>
                <Avatar sx={{ width: 36, height: 36, backgroundColor: 'primary.main', fontSize: '14px' }}>
                  {userInitials}
                </Avatar>
              </IconButton>

              <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleMenuClose}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                transformOrigin={{ vertical: 'top', horizontal: 'right' }}
              >
                <MenuItem disabled>
                  <Typography variant="body2" sx={{ fontWeight: 600 }}>
                    {userName}
                  </Typography>
                </MenuItem>
                <MenuItem onClick={() => { navigate('/profile'); handleMenuClose(); }}>
                  <ProfileIcon sx={{ mr: 1, fontSize: '20px' }} />
                  Profile
                </MenuItem>
                <MenuItem onClick={handleLogout}>
                  <LogoutIcon sx={{ mr: 1, fontSize: '20px' }} />
                  Logout
                </MenuItem>
              </Menu>
            </Box>
          </Toolbar>
        </AppBar>

        <Box component="main" sx={{ flex: 1, overflow: 'auto', p: { xs: 2, md: 3 } }}>
          <Container maxWidth="xl" disableGutters>
            {children}
          </Container>
        </Box>
      </Box>
    </Box>
  );
};

export default MainLayout;
