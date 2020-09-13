import { CheckoutRoutingModule } from './checkout-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutListComponent } from './checkout-list/checkout-list.component';
import { CheckoutDetailComponent } from './checkout-detail/checkout-detail.component';

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { SkuComponent } from './checkout-detail/sku/sku.component';

@NgModule({
  declarations: [CheckoutListComponent, CheckoutDetailComponent, SkuComponent],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    MatIconModule,
    MatButtonModule,
  ],
})
export class CheckoutModule {}
