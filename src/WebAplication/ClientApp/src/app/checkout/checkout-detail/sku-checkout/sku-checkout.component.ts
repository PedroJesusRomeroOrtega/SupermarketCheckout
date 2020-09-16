import { CheckoutService } from '../../checkout.service';
import {
  Component,
  OnInit,
  Input,
  ChangeDetectionStrategy,
  OnDestroy,
} from '@angular/core';
import { CheckoutUnit, SkuWithCheckoutUnit } from '../../models';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-sku-checkout',
  templateUrl: './sku-checkout.component.html',
  styleUrls: ['./sku-checkout.component.scss'],
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SkuCheckoutComponent implements OnInit, OnDestroy {
  @Input() skuWithCheckoutUnit: SkuWithCheckoutUnit;
  private destroyed$: Subject<void> = new Subject();

  constructor(private checkoutService: CheckoutService) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.destroyed$.next();
    this.destroyed$.complete();
  }

  addUnit(): void {
    const checkoutUnit: CheckoutUnit = {
      checkoutId: this.skuWithCheckoutUnit?.checkoutId ?? null,
      skuId: this.skuWithCheckoutUnit.skuId,
      numberOfUnits: 1,
      totalPrice: null,
    };
    this.checkoutService
      .addUnit(checkoutUnit)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (checkoutUnitResult) =>
          (this.skuWithCheckoutUnit = {
            ...this.skuWithCheckoutUnit,
            ...checkoutUnitResult,
          })
      );
  }
}
