import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoanCalculatorComponent } from './pages/loan-calculator/loan-calculator.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'loan-calculator', component: LoanCalculatorComponent },
];
