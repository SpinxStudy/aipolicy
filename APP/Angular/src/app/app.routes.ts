import { Routes } from '@angular/router';
import { policyRoutes } from './features/policy/policy.route';

export const appRoutes: Routes = [
  { path: '', redirectTo: '/policies', pathMatch: 'full' },
  ...policyRoutes,
  { path: '**', redirectTo: '/policies' }
];