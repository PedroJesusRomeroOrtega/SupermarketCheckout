import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { map, tap } from 'rxjs/operators';

import { CheckoutService } from '../checkout.service';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-checkout-detail',
  templateUrl: './checkout-detail.component.html',
  styleUrls: ['./checkout-detail.component.scss'],
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CheckoutDetailComponent implements OnInit {
  checkoutId$ = this.route.paramMap
    .pipe(
      tap((params: ParamMap) => console.log(params)),
      map((params: ParamMap) => (params.get('id') ? +params.get('id') : null)),
      tap((id: number) => this.checkoutService.selectCheckout(id))
    )
    .subscribe();

  checkout$ = this.checkoutService.checkoutSeleted$;
  skusWithCheckoutUnits$ = this.checkoutService.skusWithCheckoutUnits$;

  vm$ = combineLatest([this.checkout$, this.skusWithCheckoutUnits$]).pipe(
    map(([checkout, skusWithCheckoutUnits]) => ({
      checkout,
      skusWithCheckoutUnits,
    }))
  );

  constructor(
    private route: ActivatedRoute,
    private checkoutService: CheckoutService
  ) {}

  ngOnInit(): void {}
}
