import { ProductModel } from './ProductModel';

export interface CategoryModel {
    id: number;
    name: string;
    checked: boolean;
    productId: number;
    products: ProductModel[];
}

export function resetCategory() {
    return {
        id: 0,
        name: '',
        checked: false,
        productId: 0,
        products: []
    };
}