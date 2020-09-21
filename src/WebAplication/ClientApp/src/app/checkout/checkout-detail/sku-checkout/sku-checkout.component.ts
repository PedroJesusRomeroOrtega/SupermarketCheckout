import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

import { SkuWithCheckoutUnit } from '../../models';
import { CheckoutService } from '../../checkout.service';

@Component({
  selector: 'app-sku-checkout',
  templateUrl: './sku-checkout.component.html',
  styleUrls: ['./sku-checkout.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SkuCheckoutComponent {
  @Input() skuWithCheckoutUnit: SkuWithCheckoutUnit;

  constructor(private checkoutService: CheckoutService) {}

  addUnit(): void {
    this.checkoutService.addUnit(this.skuWithCheckoutUnit);
  }
}
