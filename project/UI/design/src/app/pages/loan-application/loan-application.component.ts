import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { DecimalPipe, NgClass, NgFor, NgIf } from '@angular/common';
import { LoanSearch } from '../../../models/LoanSeach.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-loan-application',
  imports: [FormsModule, HttpClientModule, ReactiveFormsModule, NgFor, NgIf, NgClass, DecimalPipe],
  templateUrl: './loan-application.component.html',
  styleUrl: './loan-application.component.css'
})
export class LoanApplicationComponent implements OnInit {
  loanAppForm = new FormGroup({
    Email: new FormControl("", [
      Validators.required,
      Validators.email, 
      Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/) 
    ]),
    NameSurname: new FormControl("", [
      Validators.required,
      Validators.pattern(/^[a-zA-ZğüşöçıİĞÜŞÖÇ]+\s+[a-zA-ZğüşöçıİĞÜŞÖÇ]+$/) 
    ]),
    BankName: new FormControl("", Validators.required),
    LoanType: new FormControl("", Validators.required),
    LoanAmount: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(1000) 
    ]),
    LoanPeriod: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(6) 
    ])
  });

  http = inject(HttpClient);
  allSearchedLoans: LoanSearch[]  = [];

  ngOnInit() {
    this.getAllSearchedLoans();
  }

  get emailControl() {
    return this.loanAppForm.get('Email');
  }

  get nameControl() {
    return this.loanAppForm.get('NameSurname');
  }

  get amountControl() {
    return this.loanAppForm.get('LoanAmount');
  }

  get periodControl() {
    return this.loanAppForm.get('LoanPeriod');
  }

  onFormSubmit() {
    if (this.loanAppForm.invalid) {
      console.log('Form is invalid!');
      return;
    }

    const addLoanCalclRequest = this.loanAppForm.value;

    this.http.post("http://localhost:5025/api/Loans", addLoanCalclRequest)
      .subscribe({
        next: (value: any) => {
          console.log('POST request successful', value);
          this.loanAppForm.reset();
          this.getAllSearchedLoans(); // ✅ Refresh search list after submission
        }
      });
  }

  private getAllSearchedLoans(){
    this.http.get<LoanSearch[]>("http://localhost:5025/api/LoanSearchs")
    .subscribe({
      next: (response) => {
        console.log ('GET request successful', response);
        this.allSearchedLoans = response;
      },
      error: (error) => {
        console.error('Error: GET', error);
      }
    });
  }
}
