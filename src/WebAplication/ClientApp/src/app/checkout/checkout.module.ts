import { CheckoutRoutingModule } from './checkout-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutListComponent } from './checkout-list/checkout-list.component';
import { CheckoutDetailComponent } from './checkout-detail/checkout-detail.component';
import { SkuCheckoutComponent } from './checkout-detail/sku-checkout/sku-checkout.component';

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatBadgeModule } from '@angular/material/badge';

@NgModule({
  declarations: [
    CheckoutListComponent,
    CheckoutDetailComponent,
    SkuCheckoutComponent,
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatBadgeModule,
  ],
})
export class CheckoutModule {}
