export interface Condition {
    id: number;
    idTrigger: number;
    type: number;
    value: string | null;
    conditionLeft?: Condition;
    conditionLeftId: number;
    subNodeL: number;
    conditionRight?: Condition;
    conditionRightId: number;
    subNodeR: number;
    lastChange: string;
  }