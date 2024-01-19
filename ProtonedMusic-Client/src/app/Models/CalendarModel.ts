import { ArtistModel, resetArtist } from "./ArtistModel";

export interface CalendarModel {
    id: number;
    title: string;
    content: string;
    date: any;
    artistId: number;
    artist: ArtistModel;
}

export function resetCalendar() {
    return {
        id: 0,
        title: '',
        content: '',
        date: new Date,
        artistId: 0,
        artist: resetArtist()
    };
}