export interface NewsModel {
    id: number;
    title: string;
    text: string;
    dateTime: any;
}

export function resetNews() {
    return {
        id: 0,
        title: '',
        text: '',
        dateTime: new Date,
    }
}