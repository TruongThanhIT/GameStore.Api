import axios from "axios";
import type { Game } from "../entities/Game";
import type { Genre } from "../entities/Genre";

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL
});

export const getGames = () => apiClient.get<Game[]>("/games").then(res => res.data);
export const createGame = (game: Omit<Game, "id">) => apiClient.post("/games", game);
export const updateGame = (game: Game) => apiClient.put(`/games/${game.id}`, game);
export const deleteGame = (id: number) => apiClient.delete(`/games/${id}`);
export const getGenres = () => apiClient.get<Genre[]>("/genres");