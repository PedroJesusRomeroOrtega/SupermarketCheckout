import { CheckoutListComponent } from './checkout-list/checkout-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckoutDetailComponent } from './checkout-detail/checkout-detail.component';

const checkoutRoutes: Routes = [
  { path: 'checkouts', component: CheckoutListComponent },
  { path: 'checkout-detail', component: CheckoutDetailComponent },
];

@NgModule({
  imports: [RouterModule.forChild(checkoutRoutes)],
  exports: [RouterModule],
})
export class CheckoutRoutingModule {}
