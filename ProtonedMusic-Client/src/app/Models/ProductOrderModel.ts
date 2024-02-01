export interface ProductOrderModel {
    id: number;
    name: string;
    price: number;
    isDiscounted: boolean;
    discountProcent: number;
    beforePrice: number;
    quantity: number;
}

export function resetOrderProducts() {
    return {
        id: 0,
        name: '',
        price: 0,
        isDiscounted: false,
        discountProcent: 0,
        beforePrice: 0,
        quantity: 0,
    }
}