import { CheckoutUnit } from './../../checkout-unit';
import { CheckoutService } from './../../checkout.service';
import { Component, OnInit, Input } from '@angular/core';
import { Sku } from '../sku';

@Component({
  selector: 'app-sku',
  templateUrl: './sku.component.html',
  styleUrls: ['./sku.component.scss'],
})
export class SkuComponent implements OnInit {
  @Input() sku: Sku;
  checkoutUnit: CheckoutUnit;

  constructor(private checkoutService: CheckoutService) {}

  ngOnInit(): void {}

  addUnit(): void {
    const checkoutUnit: CheckoutUnit = {
      checkoutId: this.checkoutUnit?.checkoutId ?? null,
      skuId: this.sku.id,
      numberOfUnits: 1,
      totalPrice: null,
    };
    this.checkoutService
      .addUnit(checkoutUnit)
      .subscribe(
        (checkoutUnitResult) => (this.checkoutUnit = checkoutUnitResult)
      );
  }
}
