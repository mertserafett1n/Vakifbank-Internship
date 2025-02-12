import { Component } from '@angular/core';
import { HomeComponent } from '../../pages/home/home.component';
import { RouterModule } from '@angular/router';
import { LoanCalculatorComponent } from '../../pages/loan-calculator/loan-calculator.component';
import { NgModule } from '@angular/core';
@Component({
  selector: 'app-navbar',
  imports: [HomeComponent,RouterModule, LoanCalculatorComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

}
