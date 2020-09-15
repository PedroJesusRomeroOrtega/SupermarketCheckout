import { CheckoutUnit } from './../../checkout-unit';
import { CheckoutService } from './../../checkout.service';
import { Component, OnInit, Input } from '@angular/core';
import { Sku } from '../sku';
import { SkuWithCheckoutUnit } from '../../sku-with-checkout-unit';

@Component({
  selector: 'app-sku',
  templateUrl: './sku.component.html',
  styleUrls: ['./sku.component.scss'],
})
export class SkuComponent implements OnInit {
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
