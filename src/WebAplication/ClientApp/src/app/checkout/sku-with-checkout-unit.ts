export interface SkuWithCheckoutUnit {
  skuId: number;
  skuName: string;
  checkoutId?: number;
  numberOfUnits?: number;
  totalPrice?: number;
}
