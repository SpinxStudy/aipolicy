import { Route } from '@angular/router';
import { PolicyListComponent } from './policy-list/policy-list.component';
import { PolicyFormComponent } from './policy-form/policy-form.component';

export const policyRoutes: Route[] = [
  { path: 'policies', component: PolicyListComponent },
  { path: 'policy/new', component: PolicyFormComponent },
  { path: 'policy/:id/edit', component: PolicyFormComponent }
];