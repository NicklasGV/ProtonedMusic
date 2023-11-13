import { ProductModel } from "./ProductModel";

export interface Cart {
    items: Array<CartItem>;
}

export interface CartItem {
    id: number;
    name: string;
    price: number;
    quantity: number;
    productModel: ProductModel;
}
