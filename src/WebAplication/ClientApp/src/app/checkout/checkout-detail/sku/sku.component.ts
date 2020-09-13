import { Component, OnInit, Input } from '@angular/core';
import { Sku } from '../sku';

@Component({
  selector: 'app-sku',
  templateUrl: './sku.component.html',
  styleUrls: ['./sku.component.scss'],
})
export class SkuComponent implements OnInit {
  @Input() sku: Sku;
  constructor() {}

  ngOnInit(): void {}
}
