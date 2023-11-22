import { CategoryModel } from './CategoryModel';

export interface ProductModel {
    id: number;
    name: string;
    price: number;
    description: string;
    image: string;
    productPicturePath: string;
    categories: CategoryModel[];
    categoryIds: number[];
}

export function resetProducts() {
    return {
        id: 0,
        name: '',
        price: 0,
        description: '',
        image: '',
        productPicturePath: '',
        categories: [],
        categoryIds: [],
    }
}