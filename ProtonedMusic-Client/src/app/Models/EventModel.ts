export interface EventModel {
    id: number,
    title: string,
    description: string,
    price: number,
    pictureFile: File | null;
    eventPicturePath: string;
    timeofEvent: any,
    dateofEvent: any,
    created: Date,
}

export function resetEvent() {
    return {
        id: 0,
        title: '',
        description: '',
        price: 0,
        pictureFile: null,
        eventPicturePath: '',
        timeofEvent: new Date(),
        dateofEvent: new Date(),
        created: new Date(),
    }
}