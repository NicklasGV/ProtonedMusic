export interface CalendarModel {
    id: number;
    title: string;
    content: string;
    date: Date;
    familyMember: string;
}

export function resetCalendar() {
    return {
        id: 0,
        title: '',
        content: '',
        date: new Date,
        familyMember: ''
    };
}