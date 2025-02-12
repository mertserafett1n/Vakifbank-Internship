import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms'; // ✅ Import FormsModule
import { Calculation } from '../../../models/calculation.model';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { NgModule } from '@angular/core';
import {inject } from '@angular/core';
import { Component } from '@angular/core';
import { PaymentPlan } from '../../../models/PaymentPlan.model';


@Component({
  selector: 'app-loan-calculator',
  standalone: true, // ✅ Important for standalone component
  imports: [FormsModule,AsyncPipe, ReactiveFormsModule, HttpClientModule], // ✅ Include FormsModule here
  templateUrl: './loan-calculator.component.html',
  styleUrls: ['./loan-calculator.component.css']
})



export class LoanCalculatorComponent {
  loanCalculForm = new FormGroup({
    LoanAmount: new FormControl<number>(0),
    LoanPeriod: new FormControl<number>(0),
    InterestRate: new FormControl<number>(0)
  })

  paymentPlanForm = new FormGroup({
    PaymentNo: new FormControl<number>(0),
    PaymentAmount: new FormControl<number>(0),
    AmountOfInterest: new FormControl<number>(0),
    AmountOfMoney: new FormControl<number>(0),
    RestOfMoney: new FormControl<number>(0)
  })
  http = inject(HttpClient);

  onFormSubmit(){
    const addLoanCalclRequest = {
      LoanAmount: this.loanCalculForm.value.LoanAmount,
      LoanPeriod: this.loanCalculForm.value.LoanPeriod,
      InterestRate: this.loanCalculForm.value.InterestRate,
    }
    this.http.post("http://localhost:5025/api/Calculations", addLoanCalclRequest)
    .subscribe({
      next: (value) => {
        console.log('POST request successful', value);
        this.loanCalculForm.reset();
      }
    });
    /*
    const showPaymentPlan = {
      PaymentId: this.paymentPlanForm.value.PaymentNo,
      PaymentAmount: this.paymentPlanForm.value.PaymentAmount,
      AmountOfInterest: this.paymentPlanForm.value.AmountOfInterest,
      AmountOfMoney: this.paymentPlanForm.value.AmountOfMoney,
      RestOfMoney: this.paymentPlanForm.value.RestOfMoney,
    }
    this.http.get("http://localhost:5025/api/Calculations")
    .subscribe({
      next: (response) => {
        console.log('GET request successful', response);
        this.paymentPlanForm.reset();
      },
      error: (error) => {
        console.error('Error:', error);
      }
    }) 
*/
  }
  /*
  private getPaymentPlan(): Observable<PaymentPlan[]> {
    return this.http.get<PaymentPlan[]>("http://localhost:5025/api/Calculations");
  }
  */

}
