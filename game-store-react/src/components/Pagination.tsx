import type { PagedList } from "../entities/PagedList";

interface Props {
  metadata: PagedList<any> | null;
  onPageChange: (page: number) => void;
}

const Pagination = ({ metadata, onPageChange }: Props) => {
  if (!metadata || metadata.totalPages <= 1) return null;

  return (
    <div className="d-flex justify-content-between align-items-center mt-4">
      <div className="text-muted">
        Showing page {metadata.pageNumber} of {metadata.totalPages} ({metadata.totalCount} items)
      </div>
      <nav>
        <ul className="pagination mb-0">
          <li className={`page-item ${!metadata.hasPreviousPage ? 'disabled' : ''}`}>
            <button 
              className="page-link" 
              onClick={() => onPageChange(metadata.pageNumber - 1)}
            >
              Previous
            </button>
          </li>
          
          <li className="page-item active">
            <span className="page-link">{metadata.pageNumber}</span>
          </li>

          <li className={`page-item ${!metadata.hasNextPage ? 'disabled' : ''}`}>
            <button 
              className="page-link" 
              onClick={() => onPageChange(metadata.pageNumber + 1)}
            >
              Next
            </button>
          </li>
        </ul>
      </nav>
    </div>
  );
};

export default Pagination;