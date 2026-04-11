export interface Game {
  id: number;
  name: string;
  genreName: string;
  genreId: number;
  price: number;
  releaseDate: string;
}

export type GameFormData = Omit<Game, 'id'>;