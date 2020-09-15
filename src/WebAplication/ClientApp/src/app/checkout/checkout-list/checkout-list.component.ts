import { CheckoutService } from './../checkout.service';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

// TODO: review the changeDetection strategy
@Component({
  selector: 'app-checkout-list',
  templateUrl: './checkout-list.component.html',
  styleUrls: ['./checkout-list.component.scss'],
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CheckoutListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date', 'totalPrice'];
  checkouts$ = this.checkoutService.checkouts$;

  constructor(private checkoutService: CheckoutService) {}

  ngOnInit(): void {}
}
