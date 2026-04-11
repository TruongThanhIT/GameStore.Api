const GameTableSkeleton = () => {
  const skeletonRows = Array(5).fill(0);

  return (
    <table className="table table-striped table-bordered mt-3">
      <thead className="table-dark">
        <tr>
          <th>Name</th>
          <th>Genre</th>
          <th>Price</th>
          <th>Release Date</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {skeletonRows.map((_, index) => (
          <tr key={index}>
            <td className="placeholder-glow">
              <span className="placeholder col-8"></span>
            </td>
            <td className="placeholder-glow">
              <span className="placeholder col-6"></span>
            </td>
            <td className="placeholder-glow">
              <span className="placeholder col-4"></span>
            </td>
            <td className="placeholder-glow">
              <span className="placeholder col-5"></span>
            </td>
            <td className="placeholder-glow">
              <span className="placeholder col-3 me-2"></span>
              <span className="placeholder col-3"></span>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default GameTableSkeleton;