import axios from "axios";

export interface Game {
  title: string;
  id: number;
  name: string;
  genre: string;
  price: number;
  releaseDate: string;
}

const apiClient = axios.create({
  baseURL: "http://localhost:5038",
});

export const getGames = () => apiClient.get<Game[]>("/games");