import type { Game } from "../entities/Game";

interface Props {
  items: Game[];
  heading: string;
  onEdit: (item: Game) => void;
  onDelete: (id: number) => void;
}

function GameTable({ items, heading, onEdit, onDelete }: Props) {
  return (
    <>
      <h2 className="my-3">{heading}</h2>
      {items.length === 0 ? (
        <p>No games available.</p>
      ) : (
        <table className="table table-bordered">
          <thead className="table-dark">
            <tr>
              <th>Name</th>
              <th>Genre</th>
              <th>Price</th>
              <th>Release Date</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {items.map((item) => (
              <tr key={item.id}>
                <td>{item.name}</td>
                <td>{item.genreName}</td>
                <td>
                  {item.price.amount.toFixed(2)} {item.price.currency}
                </td>
                <td>
                  {new Intl.DateTimeFormat("en-NZ").format(
                    new Date(item.releaseDate),
                  )}
                </td>
                <td className="text-center">
                  <button
                    className="btn btn-outline-primary btn-sm me-2"
                    onClick={() => onEdit(item)}
                    title="Edit"
                  >
                    <i className="bi bi-pencil-fill"></i>
                  </button>
                  <button
                    className="btn btn-outline-danger btn-sm"
                    onClick={() => onDelete(item.id)}
                    title="Delete"
                  >
                    <i className="bi bi-trash-fill"></i>
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
}

export default GameTable;
