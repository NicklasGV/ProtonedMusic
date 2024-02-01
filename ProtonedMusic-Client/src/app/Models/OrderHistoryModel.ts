import { ProductOrderModel } from "./ProductOrderModel";

export interface OrderHistoryModel{
  id?: number;
  orderDate: Date;
  products: ProductOrderModel[];
}
export function resetOrderHistory() {
  return {
    id: 0,
    orderDate: new Date(),
    products: [],
  };
}
