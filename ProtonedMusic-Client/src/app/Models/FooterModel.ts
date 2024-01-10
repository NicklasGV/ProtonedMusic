export interface FooterModel {
    id: number,
    description: string,
    address: string,
    addressMapLink: string,
    mail: string,
    phonenumber: string,
}

export function resetFooter() {
    return {
        id: 0,
        description: '',
        address: '',
        addressMapLink: '',
        mail: '',
        phonenumber: '',
    }
}