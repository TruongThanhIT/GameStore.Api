import Alert from "./components/Alert";
import GameButton from "./components/GameButton";
import { useEffect, useState } from "react";
import GameTable from "./components/GameTable";
import type { Game, GameFormData } from "./entities/Game";
import { createGame, deleteGame, getGames, updateGame } from "./api/gameClient";
import GameForm from "./components/GameForm";
import GameTableSkeleton from "./components/GameTableSkeleton";

function App() {
  const [games, setGames] = useState<Game[]>([]);
  const [showAlert, setShowAlert] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [selectedGame, setSelectedGame] = useState<Game | undefined>(undefined);
  const [isFetching, setIsFetching] = useState(false); 
  const [isSaving, setIsSaving] = useState(false);

  const fetchGames = async () => {
    setIsFetching(true);
    try {
      // const response = await getGames();
      const [response] = await Promise.all([
      getGames(),
      new Promise(resolve => setTimeout(resolve, 2000)) 
    ]);
      setGames(response);
    } catch (error) {
      console.error("Error fetching games:", error);
      setError("Could not load games. Is the Backend running?");
    } finally {
      setIsFetching(false);
    }
  };

  useEffect(() => {
    fetchGames();
  }, []);

  const handleCreateGame = () => {
    setSelectedGame(undefined);
    setShowForm(true);
  };

  const handleEditGame = (game: Game) => {
    setSelectedGame(game);
    setShowForm(true);
  };

  const handleDeleteGame = async (id: number) => {
    try {
      await deleteGame(id);
      setAlertMessage("Game deleted successfully!");
      setShowAlert(true);
      await fetchGames();
    } catch (error) {
      setError("Could not delete game. Please try again.");
    }
  };

  const handleSavedGame = async (data: GameFormData) => {
    setIsSaving(true);
    try {
      if (selectedGame) {
        await updateGame({ ...selectedGame, ...data });
        setAlertMessage("Game saved successfully!");
      } else {
        await createGame(data);
        setAlertMessage("Game created successfully!");
      }
      await fetchGames();
      setShowForm(false);
      setShowAlert(true);
    } catch (error) {
      setError("Could not save game. Please try again.");
    } finally {
      setIsSaving(false);
    }
  };

  return (
    <div>
      {error && <div className="alert alert-danger">{error}</div>}
      <Alert show={showAlert} onClose={() => setShowAlert(false)}>
        {alertMessage}
      </Alert>
      <div className="container mt-4">
        {showForm ? (
          <GameForm
            onSubmit={handleSavedGame}
            onCancel={() => setShowForm(false)}
            initialData={selectedGame}
            isLoading={isSaving}
          />
        ) : (
          <>
            <div className="d-flex justify-content-between align-items-center mb-3">
              <GameButton className="mt-3" onClick={handleCreateGame}>
                Create New Game
              </GameButton>
            </div>
            {isFetching ? (
              <GameTableSkeleton />
            ) : (
              <GameTable
                items={games}
                heading="Games"
                onEdit={handleEditGame}
                onDelete={handleDeleteGame}
              />
            )}
          </>
        )}
      </div>
    </div>
  );
}

export default App;
