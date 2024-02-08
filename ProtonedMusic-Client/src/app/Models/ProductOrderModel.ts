export interface ProductOrderModel {
    id: number;
    orderId: number;
    name: string;
    price: number;
    quantity: number;
}

export function resetOrderProducts() {
    return {
        id: 0,
        orderId: 0,
        name: '',
        price: 0,
        quantity: 0,
    }
}