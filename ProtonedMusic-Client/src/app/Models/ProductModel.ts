import { CategoryModel } from './CategoryModel';

export interface ProductModel {
    id: number;
    name: string;
    price: number;
    description: string;
    pictureFile: File | null;
    productPicturePath: string;
    categories: CategoryModel[];
    categoryIds: number[];
    isDiscounted: boolean;
    discountProcent: number;
}

export function resetProducts() {
    return {
        id: 0,
        name: '',
        price: 0,
        description: '',
        image: '',
        pictureFile: null,
        productPicturePath: '',
        categories: [],
        categoryIds: [],
        isDiscounted: false,
        discountProcent: 0,
    }
}