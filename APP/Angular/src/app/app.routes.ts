import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: 'policies', loadComponent: () => import('./features/policy/policy-list/policy-list.component').then(m => m.PolicyListComponent) }
  ];