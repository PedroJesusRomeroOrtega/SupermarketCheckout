import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { map, switchMap, distinctUntilChanged, tap } from 'rxjs/operators';

import { CheckoutService } from '../checkout.service';
import { combineLatest, of } from 'rxjs';

@Component({
  selector: 'app-checkout-detail',
  templateUrl: './checkout-detail.component.html',
  styleUrls: ['./checkout-detail.component.scss'],
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CheckoutDetailComponent implements OnInit {
  checkoutId$ = this.route.paramMap.pipe(
    map((params: ParamMap) => (params.get('id') ? +params.get('id') : null)),
    distinctUntilChanged(),
    tap((id) => this.checkoutService.selectCheckout(id))
    // switchMap((id: number) => of(this.checkoutService.selectCheckout(id)))
  );

  checkout$ = this.checkoutService.checkoutSelected$;
  skusWithCheckoutUnits$ = this.checkoutService.skusWithCheckoutUnits$;

  vm$ = combineLatest([
    this.checkoutId$,
    this.checkout$,
    this.skusWithCheckoutUnits$,
  ]).pipe(
    map(([checkoutId, checkout, skusWithCheckoutUnits]) => ({
      checkoutId,
      checkout,
      skusWithCheckoutUnits,
    }))
  );

  constructor(
    private route: ActivatedRoute,
    private checkoutService: CheckoutService
  ) {}

  ngOnInit(): void {
    // const checkoutId = this.route.snapshot.paramMap.get('id')?this.route.snapshot.paramMap.get('id'):null;
  }
}
