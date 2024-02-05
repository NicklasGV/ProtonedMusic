import { ProductOrderModel } from "./ProductOrderModel";

export interface OrderHistoryModel{
  id: number;
  customerId?: number;
  orderDate: any;
  products: ProductOrderModel[];
}
export function resetOrderHistory() {
  return {
    id: 0,
    orderDate: new Date(),
    products: [],
  };
}
