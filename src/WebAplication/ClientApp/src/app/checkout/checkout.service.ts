import { SkuService } from './sku.service';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {
  throwError,
  Observable,
  combineLatest,
  BehaviorSubject,
  Subject,
  merge,
} from 'rxjs';
import {
  catchError,
  switchMap,
  map,
  shareReplay,
  concatMap,
  scan,
} from 'rxjs/operators';
import { Checkout, CheckoutUnit, SkuWithCheckoutUnit, Sku } from './models';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  checkouts$ = this.http
    .get<Checkout[]>(`${this.baseUrl}api/checkout`)
    .pipe(catchError(this.handleError));

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
    }),
    shareReplay()
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

  skusWithCheckoutUnits$ = combineLatest([
    this.skuService.skus$,
    this.checkoutAndItsUnits$,
  ]).pipe(
    map(([skus, { checkout, checkoutUnits }]) =>
      skus.map((s: Sku) =>
        this.createSkuWithCheckoutUnits(s, checkout.id, checkoutUnits)
      )
    )
  );

  private addSkuWithCheckoutUnitsSubject = new Subject<SkuWithCheckoutUnit>();
  addSkuCheckoutUnit$ = this.addSkuWithCheckoutUnitsSubject.asObservable();

  skusWithCheckoutUnitsWithAdd$ = merge(
    this.skusWithCheckoutUnits$,
    this.addSkuCheckoutUnit$.pipe(
      concatMap((skuCheckoutUnit: SkuWithCheckoutUnit) => {
        const checkoutUnit: CheckoutUnit = {
          checkoutId: skuCheckoutUnit?.checkoutId ?? null,
          skuId: skuCheckoutUnit.skuId,
          numberOfUnits: 1,
          totalPrice: null,
        };
        return this.http
          .post<CheckoutUnit>(
            `${this.baseUrl}api/checkout/checkoutUnits`,
            checkoutUnit,
            {
              headers: this.headers,
            }
          )
          .pipe(
            map((cu) => ({ ...skuCheckoutUnit, ...cu } as SkuWithCheckoutUnit)),
            catchError(this.handleError)
          );
      })
    )
  ).pipe(
    scan(
      (
        skusWithcheckoutUnits: SkuWithCheckoutUnit[],
        skuWithcheckoutUnits: SkuWithCheckoutUnit
      ) =>
        this.modifySkusWithCheckoutUnits(
          skusWithcheckoutUnits,
          skuWithcheckoutUnits
        )
    )
  );

  modifySkusWithCheckoutUnits(
    skusWithcheckoutUnits: SkuWithCheckoutUnit[],
    skuWithcheckoutUnits: SkuWithCheckoutUnit
  ): SkuWithCheckoutUnit[] {
    if (skusWithcheckoutUnits === null) {
      return [];
    }
    return skusWithcheckoutUnits.map((scu) =>
      scu.skuId === skuWithcheckoutUnits.skuId
        ? { ...skuWithcheckoutUnits }
        : scu
    );
  }

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    private skuService: SkuService
  ) {}

  addUnit(skuWithCheckoutUnit: SkuWithCheckoutUnit): void {
    this.addSkuWithCheckoutUnitsSubject.next(skuWithCheckoutUnit);
    // return this.http
    //   .post<CheckoutUnit>(
    //     `${this.baseUrl}api/checkout/checkoutUnits`,
    //     checkoutUnit,
    //     {
    //       headers: this.headers,
    //     }
    //   )
    //   .pipe(catchError(this.handleError));
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
        this.findCheckoutUnit(checkoutUnits, sku.id)?.numberOfUnits ?? 0,
      checkoutId,
      totalPrice: this.findCheckoutUnit(checkoutUnits, sku.id)?.totalPrice ?? 0,
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
