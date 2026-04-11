import Alert from "./components/Alert";
import GameButton from "./components/GameButton";
import { useEffect, useState } from "react";
import { getGames, type Game } from "./api/gameClient";
import GameTable from "./components/GameTable";

function App() {
  const [games, setGames] = useState<Game[]>([]);
  const [showAlert, setShowAlert] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const loadGames = async () => {
      try {
        const response = await getGames();
        setGames(response.data);
      } catch (error) {
        console.error("Error fetching games:", error);
        setError("Could not load games. Is the Backend running?");
      }
    };

    loadGames();
  }, []);

  return (
    <div>
      {error && <div className="alert alert-danger">{error}</div>}
      <Alert show={showAlert} onClose={() => setShowAlert(false)}>
        Hello <span>world</span>
      </Alert>
      <GameButton className="mt-3" onClick={() => setShowAlert(true)}>
        Create New Game
      </GameButton>
      <GameTable
        items={games}
        heading="Games"
        onEdit={(game) => console.log("Edit", game)}
        onDelete={(id) => console.log("Delete", id)}
      />
    </div>
  );
}

export default App;
