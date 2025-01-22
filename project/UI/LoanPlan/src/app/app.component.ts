import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Loan } from '../../models/loan.model';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HttpClientModule, AsyncPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  
  http = inject(HttpClient);


  loans$ = this.getLoans();

  private getLoans(): Observable<Loan[]>{
    return (this.http.get<Loan[]>("https://localhost:7047/api/Loans")); // https://stackoverflow.com/questions/48837692/type-observableobject-is-not-assignable-to-type-observableiuser
  }



}
