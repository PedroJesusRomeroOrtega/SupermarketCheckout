import { CheckoutService } from './../checkout.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-checkout-list',
  templateUrl: './checkout-list.component.html',
  styleUrls: ['./checkout-list.component.scss'],
})
export class CheckoutListComponent implements OnInit {
  checkouts$ = this.checkoutService.checkouts$;

  constructor(private checkoutService: CheckoutService) {}

  ngOnInit(): void {}
}
