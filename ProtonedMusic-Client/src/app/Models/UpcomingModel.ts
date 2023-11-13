export interface UpcomingModel {
    id: number,
    title: string,
    description: string,
    timeof: any,
    created: Date,
}

export function resetUpcoming() {
    return {
        id: 0,
        title: '',
        description: '',
        eventPicturePath: '',
        timeof: new Date(),
        created: new Date(),
    }
}