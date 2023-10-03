import { CategoryModel } from './CategoryModel';

export interface ProductModel {
    id: number;
    productName: string;
    productPrice: number;
    productDescription: string;
    productImage: string;
    categories: CategoryModel[];
    categoryIds: number[];
}

export function resetProducts() {
    return {
        id: 0,
        productName: '',
        productPrice: 0,
        productDescription: '',
        productImage: '',
        categories: [],
        categoryIds: [],
    }
}