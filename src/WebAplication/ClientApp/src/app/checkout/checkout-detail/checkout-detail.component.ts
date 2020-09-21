import { Component, ChangeDetectionStrategy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { combineLatest, of } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { SkuWithCheckoutUnit } from './../models/sku-with-checkout-unit';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-detail',
  templateUrl: './checkout-detail.component.html',
  styleUrls: ['./checkout-detail.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CheckoutDetailComponent {
  checkoutId$ = this.route.paramMap.pipe(
    map((params: ParamMap) => (params.get('id') ? +params.get('id') : null)),
    switchMap((id: number) => of(this.checkoutService.selectCheckout(id)))
  );

  vm$ = combineLatest([
    this.checkoutId$,
    this.checkoutService.checkoutSelected$,
    this.checkoutService.skusWithCheckoutUnitsWithAdd$,
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

  calculateTotal(scus: SkuWithCheckoutUnit[]): number {
    return scus.reduce((total, scu) => total + scu.totalPrice, 0);
  }
}
