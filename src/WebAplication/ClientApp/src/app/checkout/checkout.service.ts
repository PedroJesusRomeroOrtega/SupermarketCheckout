import { SkuService } from './sku.service';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError, Observable, combineLatest, BehaviorSubject } from 'rxjs';
import {
  catchError,
  switchMap,
  map,
  distinctUntilChanged,
  filter,
  share,
} from 'rxjs/operators';
import { Checkout, CheckoutUnit, SkuWithCheckoutUnit, Sku } from './models';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  checkouts$ = this.http
    .get<Checkout[]>(`${this.baseUrl}api/checkout`)
    .pipe(share(), catchError(this.handleError));

  private checkoutIdSelectedSubject = new BehaviorSubject<number>(0);
  checkoutIdSelected$: Observable<
    number
  > = this.checkoutIdSelectedSubject.asObservable().pipe(
    filter((id) => id === null || id > 0),
    distinctUntilChanged()
  );

  checkoutSelected$: Observable<Checkout> = this.checkoutIdSelected$.pipe(
    switchMap((checkoutId: number) => {
      const checkoutIdStr = checkoutId ?? '';
      return this.http
        .get<Checkout>(
          `${this.baseUrl}api/checkout/GetOrCreate/${checkoutIdStr}`
        )
        .pipe(share(), catchError(this.handleError));
    })
  );

  checkoutUnits$ = this.checkoutSelected$.pipe(
    switchMap((checkout: Checkout) =>
      this.http
        .get<CheckoutUnit[]>(
          `${this.baseUrl}api/checkout/checkoutUnits/${checkout.id}`
        )
        .pipe(
          share(),
          map((checkoutUnits) => ({ checkout, checkoutUnits })),
          catchError(this.handleError)
        )
    )
  );

  skusWithCheckoutUnits$: Observable<SkuWithCheckoutUnit[]> = combineLatest([
    this.skuService.skus$,
    this.checkoutUnits$,
  ]).pipe(
    map(([skus, { checkout, checkoutUnits }]) =>
      skus.map((s: Sku) =>
        this.createSkuWithCheckoutUnits(s, checkout.id, checkoutUnits)
      )
    )
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
