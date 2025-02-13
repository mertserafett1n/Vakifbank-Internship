import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms'; // ✅ Import FormsModule
import { Calculation } from '../../../models/calculation.model';
import { Observable } from 'rxjs';
import { AsyncPipe, DecimalPipe, NgFor, NgIf } from '@angular/common';
import { NgModule } from '@angular/core';
import {inject } from '@angular/core';
import { Component } from '@angular/core';
import { PaymentPlan } from '../../../models/PaymentPlan.model';


@Component({
  selector: 'app-loan-calculator',
  standalone: true, // ✅ Important for standalone component
  imports: [FormsModule,AsyncPipe, ReactiveFormsModule, HttpClientModule, NgFor, NgIf, DecimalPipe],
  templateUrl: './loan-calculator.component.html',
  styleUrls: ['./loan-calculator.component.css']
})



export class LoanCalculatorComponent {
  loanCalculForm = new FormGroup({
    LoanAmount: new FormControl<number>(0),
    LoanPeriod: new FormControl<number>(0),
    InterestRate: new FormControl<number>(0)
  })

  http = inject(HttpClient);
  paymentPlanList: PaymentPlan[] = []; // Stores all payment plans for table display
  onFormSubmit(){
    var tempId: string;
    const addLoanCalclRequest = {
      LoanAmount: this.loanCalculForm.value.LoanAmount,
      LoanPeriod: this.loanCalculForm.value.LoanPeriod,
      InterestRate: this.loanCalculForm.value.InterestRate,
    }
    this.http.post("http://localhost:5025/api/Calculations", addLoanCalclRequest)
    .subscribe({
      next: (value: any) => {
        console.log('POST request successful, The Id of Last Data: ', value );
        this.loanCalculForm.reset();
        tempId = value;
        this.getPaymentPlan(tempId);
      }
    });
  }
  

// Getting 400 error, nothing bad w posting, smth wrong w get method. Might need to with here. 
  getPaymentPlan(calculationId: string){
    if(!calculationId || calculationId.trim() == ""){
      console.error('Invaid calculationID: ', calculationId);
      return;
    }

    const apiURL = `http://localhost:5025/api/Calculations/${calculationId}`;
    console.log(`GET Request to: ${apiURL}`); // Debugging if the apiurl matches the last Id well.



    this.http.get<PaymentPlan[]>(apiURL)
    .subscribe({
      next: (response) => {
        console.log ('GET request successful', response);
        if(response.length > 0){
          this.paymentPlanList = response;
        }
      },
      error: (error) => {
        console.error('Error: GET METHOD LOANCALC. !=', error);
      }
    });
  }

}
