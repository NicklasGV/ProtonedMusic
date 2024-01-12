import { ItemProduct } from "./ItemProductModel";

export interface OrderHistory{
  id: number;
  orderNumber: number;
  items: ItemProduct[];
  price: number;
  quantity: number;
  orderDate: Date;
}
export function resetOrderHistory() {
  return {
    id: 0,
    orderNumber: 0,
    items: [],
    price: 0,
    quantity: 0,
    orderDate: new Date()
  };
}
