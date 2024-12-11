import { Routes } from '@angular/router';
import { ProblemsComponent } from './problems/problems.component'; // Adjust the path to where your ProblemsComponent is located

export const routes: Routes = [
  { path: '', component: ProblemsComponent }, // Default route
  { path: 'problems', component: ProblemsComponent }, // Explicit route for problems (if needed)
];
