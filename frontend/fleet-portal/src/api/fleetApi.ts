import { apiClient } from './apiClient';
import type { ApiResponse, DashboardOverview, Driver, Vehicle } from './types';

export const fetchDashboardStats = async () => {
  const response = await apiClient.get<ApiResponse<DashboardOverview>>('/fleet/Dashboard/stats');
  if (!response.data.data) {
    throw new Error(response.data.message || 'Dashboard data unavailable');
  }
  return response.data.data;
};

export const fetchVehicles = async () => {
  const response = await apiClient.get<ApiResponse<Vehicle[]>>('/fleet/vehicles');
  return response.data.data ?? [];
};

export const fetchVehicleById = async (id: string) => {
  const response = await apiClient.get<ApiResponse<Vehicle>>(`/fleet/vehicles/${id}`);
  return response.data.data;
};

export const createVehicle = async (vehicleData: Omit<Vehicle, 'id' | 'status' | 'createdAt'>) => {
  const response = await apiClient.post<ApiResponse<Vehicle>>('/fleet/vehicles', vehicleData);
  return response.data.data;
};

export const fetchDrivers = async () => {
  const response = await apiClient.get<ApiResponse<Driver[]>>('/fleet/drivers');
  return response.data.data ?? [];
};

export const fetchDriverById = async (id: string) => {
  const response = await apiClient.get<ApiResponse<Driver>>(`/fleet/drivers/${id}`);
  return response.data.data;
};
