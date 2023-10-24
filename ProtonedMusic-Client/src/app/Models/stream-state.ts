export interface StreamState {
    playing: boolean;
    readableCurrentTime: string | '0:00';
    readableDuration: string | '0:00';
    duration: number | undefined;
    currentTime: number | undefined;
    canplay: boolean;
    error: boolean;
    currentSong: string;
  }