import { SkuService } from './sku.service';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError, Observable, combineLatest, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, map, tap } from 'rxjs/operators';
import { Checkout, CheckoutUnit, SkuWithCheckoutUnit, Sku } from './models';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  checkouts$ = this.http
    .get<Checkout[]>(`${this.baseUrl}api/checkout`)
    .pipe(tap(console.log), catchError(this.handleError));

  private checkoutIdSelectedSubject = new BehaviorSubject<number>(0);
  checkoutIdSelected$ = this.checkoutIdSelectedSubject.asObservable();

  checkoutSelected$: Observable<Checkout> = this.checkoutIdSelected$.pipe(
    switchMap((checkoutId: number) => {
      const checkoutIdStr = checkoutId ?? '';
      return this.http
        .get<Checkout>(
          `${this.baseUrl}api/checkout/GetOrCreate/${checkoutIdStr}`
        )
        .pipe(catchError(this.handleError));
    })
  );

  checkoutAndItsUnits$ = this.checkoutSelected$.pipe(
    switchMap((checkout: Checkout) =>
      this.http
        .get<CheckoutUnit[]>(
          `${this.baseUrl}api/checkout/checkoutUnits/${checkout.id}`
        )
        .pipe(
          map((checkoutUnits) => ({ checkout, checkoutUnits })),
          catchError(this.handleError)
        )
    )
  );

  checkoutAndSkusWithUnits$ = combineLatest([
    this.skuService.skus$,
    this.checkoutAndItsUnits$,
  ]).pipe(
    map(([skus, { checkout, checkoutUnits }]) => ({
      checkout,
      skusWithCheckoutUnits: skus.map((s: Sku) =>
        this.createSkuWithCheckoutUnits(s, checkout.id, checkoutUnits)
      ),
    }))
  );

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    private skuService: SkuService
  ) {}

  addUnit(checkoutUnit: CheckoutUnit): Observable<CheckoutUnit> {
    return this.http
      .post<CheckoutUnit>(
        `${this.baseUrl}api/checkout/checkoutUnits`,
        checkoutUnit,
        {
          headers: this.headers,
        }
      )
      .pipe(catchError(this.handleError));
  }

  selectCheckout(checkoutId: number): void {
    this.checkoutIdSelectedSubject.next(checkoutId);
  }

  private createSkuWithCheckoutUnits(
    sku: Sku,
    checkoutId: number,
    checkoutUnits: CheckoutUnit[]
  ): SkuWithCheckoutUnit {
    return {
      skuId: sku.id,
      skuName: sku.name,
      numberOfUnits:
        this.findCheckoutUnit(checkoutUnits, sku.id)?.numberOfUnits ?? null,
      checkoutId,
      totalPrice:
        this.findCheckoutUnit(checkoutUnits, sku.id)?.totalPrice ?? null,
    } as SkuWithCheckoutUnit;
  }

  private findCheckoutUnit = (
    checkoutUnits: CheckoutUnit[],
    skuId: number
  ): CheckoutUnit => checkoutUnits.find((cu) => cu.skuId === skuId);

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
