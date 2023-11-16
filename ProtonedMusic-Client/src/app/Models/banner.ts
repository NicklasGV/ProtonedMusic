export interface Banner {
  id: number;
  name: string;
  selected: boolean;
}
  export const constBanners: Banner[] = [
    {
      id: 0, 
      name: "LeftBanner",
      selected: false
    },
    {
      id: 1,
      name: "RightBanner",
      selected: false
    },
    {
      id: 2,
      name: "MiddleBanner",
      selected: false
    }
  ]
