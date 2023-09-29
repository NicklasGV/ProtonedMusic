import { CategoryModel } from './CategoryModel';

export interface ProductModel {
    id: number;
    productName: string;
    productCategory: string;
    productPrice: number;
    productDescription: string;
    productImage: string;
}

export function resetProducts() {
    return {
        id: 0,
        productName: '',
        productCategory: '',
        productPrice: 0,
        productDescription: '',
        productImage: '',
    }
}