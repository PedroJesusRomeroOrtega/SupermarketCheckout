import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'supermarket-UI';
  skus: Sku[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  ngOnInit() {
    this.http.get<Sku[]>(this.baseUrl + 'api/sku').subscribe(
      (skus) => {
        this.skus = skus;
      },
      (error) => console.error(error)
    );
  }
}

interface Sku {
  id: number;
  name: string;
}
