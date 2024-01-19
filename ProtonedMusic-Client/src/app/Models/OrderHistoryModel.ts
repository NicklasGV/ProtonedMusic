import { ProductModel } from "./ProductModel";

export interface OrderHistoryModel{
  id: number;
  orderDate: Date;
  products: ProductModel[];
  productIds: number[];
  quantity: number;
}
export function resetOrderHistory() {
  return {
    id: 0,
    orderDate: new Date(),
    products: [],
    productIds: [],
    quantity: 0,
  };
}
