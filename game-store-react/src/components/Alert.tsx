import type { ReactNode } from "react";

interface Props {
  children: ReactNode;
  show: boolean;
  onClose: () => void;
}

const Alert = ({ children, show, onClose }: Props) => {
  return (
    show && (
      <div className="alert alert-primary alert-dismissible">
        {children}
        <button
          type="button"
          className="btn-close"
          data-bs-dismiss="alert"
          onClick={onClose}
        ></button>
      </div>
    )
  );
};

export default Alert;
