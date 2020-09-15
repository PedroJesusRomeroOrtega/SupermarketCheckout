import { SkuService } from './../sku.service';
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Sku } from './sku';

@Component({
  selector: 'app-checkout-detail',
  templateUrl: './checkout-detail.component.html',
  styleUrls: ['./checkout-detail.component.scss'],
})
export class CheckoutDetailComponent implements OnInit {
  skus$ = this.skuService.skus$;

  constructor(private skuService: SkuService) {}

  ngOnInit(): void {}
}
