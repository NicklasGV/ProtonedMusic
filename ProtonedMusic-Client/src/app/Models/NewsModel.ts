import { User } from "./UserModel";
export interface NewsModel {
    id: number;
    title: string;
    text: string;
    dateTime: any;
    newsLikes: User[];
    userIds: number[];
}

export function resetNews() {
    return {
        id: 0,
        title: '',
        text: '',
        dateTime: new Date,
        newsLikes: [],
        userIds: []
    }
}