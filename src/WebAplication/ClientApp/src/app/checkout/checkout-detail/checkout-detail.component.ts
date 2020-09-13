import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Sku } from './sku';

@Component({
  selector: 'app-checkout-detail',
  templateUrl: './checkout-detail.component.html',
  styleUrls: ['./checkout-detail.component.scss'],
})
export class CheckoutDetailComponent implements OnInit {
  skus: Sku[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  ngOnInit(): void {
    this.http.get<Sku[]>(this.baseUrl + 'api/sku').subscribe(
      (skus) => {
        this.skus = skus;
      },
      (error) => console.error(error)
    );
  }
}
