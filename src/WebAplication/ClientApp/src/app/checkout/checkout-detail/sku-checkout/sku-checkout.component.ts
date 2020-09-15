import { CheckoutService } from '../../checkout.service';
import { Component, OnInit, Input } from '@angular/core';
import { CheckoutUnit, SkuWithCheckoutUnit } from '../../models';

@Component({
  selector: 'app-sku-checkout',
  templateUrl: './sku-checkout.component.html',
  styleUrls: ['./sku-checkout.component.scss'],
})
export class SkuCheckoutComponent implements OnInit {
  @Input() skuWithCheckoutUnit: SkuWithCheckoutUnit;

  constructor(private checkoutService: CheckoutService) {}

  ngOnInit(): void {}

  addUnit(): void {
    const checkoutUnit: CheckoutUnit = {
      checkoutId: this.skuWithCheckoutUnit?.checkoutId ?? null,
      skuId: this.skuWithCheckoutUnit.skuId,
      numberOfUnits: 1,
      totalPrice: null,
    };
    this.checkoutService.addUnit(checkoutUnit).subscribe(
      (checkoutUnitResult) =>
        (this.skuWithCheckoutUnit = {
          ...this.skuWithCheckoutUnit,
          ...checkoutUnitResult,
        })
    );
  }
}
