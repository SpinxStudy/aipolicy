import { Trigger } from './trigger.model';

export interface Policy {
  id: number;
  version: number;
  name: string;
  lastChange: string; 
  triggers?: Trigger[];
}

export interface PolicyInput {
  name: string;
  version?: number; 
}