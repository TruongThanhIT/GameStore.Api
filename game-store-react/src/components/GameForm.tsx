import { useForm } from "react-hook-form";
import type { GameFormData } from "../entities/Game";
import { useEffect, useState } from "react";
import type { Genre } from "../entities/Genre";
import { getGenres } from "../api/gameClient";

interface Props {
  onSubmit: (data: GameFormData) => void;
  onCancel: () => void;
  initialData?: GameFormData;
  isLoading?: boolean;
}

function GameForm({ onSubmit, onCancel, initialData, isLoading }: Props) {
  const [error, setError] = useState<string | null>(null);
  const { register, handleSubmit, reset } = useForm<GameFormData>({
    defaultValues: initialData,
  });
  const [genres, setGenres] = useState<Genre[]>([]);
  const fetchGenres = async () => {
    try {
      const response = await getGenres();
      setGenres(response.data);
    } catch (error) {
      console.error("Error fetching genres:", error);
      setError("Could not load genres. Is the Backend running?");
    }
  };

  useEffect(() => {
    fetchGenres().then(() => {
      if (initialData) {
        reset(initialData);
      }
    });
  }, [initialData, reset]);

  return (
    <div className="container mt-3">
      {error && <div className="alert alert-danger">{error}</div>}
      <h2>{initialData ? "Edit Game" : "New Game"}</h2>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="mb-3">
          <label className="form-label">Name:</label>
          <input
            {...register("name", { required: true })}
            id="name"
            type="text"
            className="form-control"
            placeholder="Astro Bot"
          />
        </div>
        <div className="mb-3">
          <label htmlFor="genre" className="form-label">
            Genre:
          </label>
          <select
            {...register("genreId", { required: true, valueAsNumber: true })}
            className="form-select"
            id="genre"
            disabled={genres.length === 0 || isLoading}
          >
            <option value="">Select genre</option>
            {genres.map((genre) => (
              <option key={genre.id} value={genre.id}>
                {genre.name}
              </option>
            ))}
          </select>
        </div>
        <div className="mb-3">
          <label htmlFor="price" className="form-label">
            Price:
          </label>
          <input
            {...register("price", { required: true, valueAsNumber: true })}
            id="price"
            type="number"
            step="0.01"
            className="form-control"
            placeholder="59.99"
          />
        </div>
        <div className="mb-3">
          <label htmlFor="releaseDate" className="form-label">
            Release Date:
          </label>
          <input
            {...register("releaseDate", { required: true })}
            id="releaseDate"
            type="date"
            className="form-control"
          />
        </div>
        <button
          type="submit"
          className="btn btn-primary me-2"
          disabled={isLoading}
        >
          {isLoading ? (
            <>
              <span className="spinner-border spinner-border-sm me-2"></span>
              Processing...
            </>
          ) : initialData ? (
            "Update"
          ) : (
            "Create"
          )}
        </button>
        <button
          type="button"
          className="btn btn-secondary"
          disabled={isLoading}
          onClick={onCancel}
        >
          Cancel
        </button>
      </form>
    </div>
  );
}

export default GameForm;
