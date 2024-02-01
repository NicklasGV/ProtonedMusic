import { ProductModel } from "./ProductModel";
import { ProductOrderModel } from "./ProductOrderModel";

export interface OrderHistoryModel{
  id: number;
  orderDate: Date;
  products: ProductOrderModel[];
  productIds: number[];
  totalPrice: number;
}
export function resetOrderHistory() {
  return {
    id: 0,
    orderDate: new Date(),
    products: [],
    productIds: [],
    totalPrice: 0,
  };
}
