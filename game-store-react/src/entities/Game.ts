export interface GamePrice {
  amount: number;
  currency: string;
}

export interface Game {
  id: number;
  name: string;
  genreName: string;
  genreId: number;
  price: GamePrice;
  releaseDate: string;
}

export type GameFormData = Omit<Game, 'id'>;