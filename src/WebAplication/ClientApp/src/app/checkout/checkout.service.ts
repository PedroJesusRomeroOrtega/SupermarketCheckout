import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CheckoutUnit } from './checkout-unit';
import { throwError, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient
  ) {}

  addUnit(checkoutUnit: CheckoutUnit): Observable<CheckoutUnit> {
    return this.http
      .post<CheckoutUnit>(`${this.baseUrl}api/checkout`, checkoutUnit, {
        headers: this.headers,
      })
      .pipe(catchError(this.handleError));
  }

  private handleError(err: any): Observable<never> {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      errorMessage = `Backend returned code ${err.status}: ${err.message}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}
