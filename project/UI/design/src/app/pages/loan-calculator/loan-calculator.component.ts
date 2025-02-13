import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormControl, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { AsyncPipe, DecimalPipe, NgClass, NgFor, NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { PaymentPlan } from '../../../models/PaymentPlan.model';

@Component({
  selector: 'app-loan-calculator',
  standalone: true,
  imports: [FormsModule, AsyncPipe, ReactiveFormsModule, HttpClientModule, NgFor, NgIf, NgClass, DecimalPipe],
  templateUrl: './loan-calculator.component.html',
  styleUrls: ['./loan-calculator.component.css']
})

export class LoanCalculatorComponent {
  loanCalculForm = new FormGroup({
    LoanAmount: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(1000) 
    ]),
    LoanPeriod: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(6) 
    ]),
    InterestRate: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(0.1)
    ])
  });

  http = inject(HttpClient);
  paymentPlanList: PaymentPlan[] = [];
  formSubmitted = false;
  loanAmount!: number;
  loanPeriod!: number;
  interestRate!: number;

  // Getters for Validation
  get loanAmountControl() {
    return this.loanCalculForm.get('LoanAmount');
  }
  get loanPeriodControl() {
    return this.loanCalculForm.get('LoanPeriod');
  }
  get interestRateControl() {
    return this.loanCalculForm.get('InterestRate');
  }

  onFormSubmit() {
    if (this.loanCalculForm.invalid) {
      console.log('Form is invalid!');
      return;
    }

    this.loanAmount = this.loanCalculForm.value.LoanAmount!;
    this.loanPeriod = this.loanCalculForm.value.LoanPeriod!;
    this.interestRate = this.loanCalculForm.value.InterestRate!;
    this.formSubmitted = true; // ✅ Show input section

    const addLoanCalclRequest = {
      LoanAmount: this.loanAmount,
      LoanPeriod: this.loanPeriod,
      InterestRate: this.interestRate,
    };

    this.http.post("http://localhost:5025/api/Calculations", addLoanCalclRequest)
      .subscribe({
        next: (value: any) => {
          console.log('POST request successful, The Id of Last Data: ', value);
          this.getPaymentPlan(value);
        }
      });
  }

  getPaymentPlan(calculationId: string) {
    if (!calculationId || calculationId.trim() === "") {
      console.error('Invalid calculationID: ', calculationId);
      return;
    }

    const apiURL = `http://localhost:5025/api/Calculations/${calculationId}`;
    console.log(`GET Request to: ${apiURL}`);

    this.http.get<PaymentPlan[]>(apiURL)
      .subscribe({
        next: (response) => {
          console.log('GET request successful', response);
          this.paymentPlanList = response;
        },
        error: (error) => {
          console.error('Error: GET METHOD LOANCALC. !=', error);
        }
      });
  }
}
