import { apiClient } from './apiClient';
import type { ApiResponse } from './types';

export interface AiChatRequest {
  message: string;
  context?: string;
}

export interface TripDraft {
  vehicleNumber?: string | null;
  driverEmail?: string | null;
  source?: string | null;
  destination?: string | null;
  startDate?: string | null;
  endDate?: string | null;
}

export interface MaintenanceDraft {
  vehicleNumber?: string | null;
  serviceType?: string | null;
  description?: string | null;
  scheduledDate?: string | null;
  cost?: number | null;
  vendor?: string | null;
}

export interface FuelDraft {
  vehicleNumber?: string | null;
  fuelDate?: string | null;
  quantity?: number | null;
  cost?: number | null;
  odometerReading?: number | null;
}

export interface VehicleStatusDraft {
  vehicleNumber?: string | null;
  newStatus?: string | null;
  reason?: string | null;
}

export interface DriverAssignmentDraft {
  vehicleNumber?: string | null;
  driverEmail?: string | null;
  unassign?: boolean;
}

export type ActionType =
  | 'none'
  | 'scheduleTrip'
  | 'createMaintenance'
  | 'logFuel'
  | 'changeVehicleStatus'
  | 'assignDriver';

export interface ActionDraft {
  action: ActionType;
  trip?: TripDraft | null;
  maintenance?: MaintenanceDraft | null;
  fuel?: FuelDraft | null;
  vehicleStatus?: VehicleStatusDraft | null;
  driverAssignment?: DriverAssignmentDraft | null;
}

export interface AiActionPlan {
  assistantMessage: string;
  reasoning: string;
  actions: ActionDraft[];
}

export const askAiAssistant = async (request: AiChatRequest) => {
  const response = await apiClient.post<ApiResponse<string>>('/ai/AiAssistant/chat', request);
  return response.data;
};

export const planAiAction = async (request: AiChatRequest) => {
  const response = await apiClient.post<AiActionPlan>('/ai/AiAssistant/plan-action', request);
  return response.data;
};
