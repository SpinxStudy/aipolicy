import { Condition } from './condition.model';

export interface Trigger {
    id: number;
    idPolicy: number;
    version: number;
    name: string;
    run: boolean;
    active: boolean;
    attackValid: boolean;
    rootCondition?: Condition;
    lastChange: string;
  }