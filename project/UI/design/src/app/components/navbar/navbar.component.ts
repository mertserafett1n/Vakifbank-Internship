import { Component } from '@angular/core';
import { HomeComponent } from '../../pages/home/home.component';
import { RouterModule } from '@angular/router';
import { LoanCalculatorComponent } from '../../pages/loan-calculator/loan-calculator.component';
import { NgModule } from '@angular/core';
import { LoanApplicationComponent } from '../../pages/loan-application/loan-application.component';
@Component({
  selector: 'app-navbar',
  imports: [HomeComponent,RouterModule, LoanCalculatorComponent, LoanApplicationComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

}
