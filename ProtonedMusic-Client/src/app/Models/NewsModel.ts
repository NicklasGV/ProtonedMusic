export interface NewsModel {
    id: number;
    title: string;
    text: string;
    dateTime: Date;
}

export function resetNews() {
    return {
        id: 0,
        title: '',
        text: '',
        dateTime: new Date,
    }
}