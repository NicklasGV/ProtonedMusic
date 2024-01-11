import { ItemProduct } from "./ItemProductModel";

export interface OrderHistory{
  id: number;
  orderNumber: number;
  items: ItemProduct[];
  price: number;
  quantity: number;
}
