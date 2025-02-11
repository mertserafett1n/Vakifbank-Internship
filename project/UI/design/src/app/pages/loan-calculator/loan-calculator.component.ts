import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms'; // ✅ Import FormsModule
import { Calculation } from '../../../models/calculation.model';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-loan-calculator',
  standalone: true, // ✅ Important for standalone component
  imports: [FormsModule,HttpClientModule,AsyncPipe, FormsModule, ReactiveFormsModule], // ✅ Include FormsModule here
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
  }
}
