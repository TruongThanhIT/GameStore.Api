interface Props {
  children: string;
  color?:
    | "primary"
    | "secondary"
    | "success"
    | "danger"
    | "warning"
    | "info"
    | "light"
    | "dark";
  onClick: () => void;
  className?: string;
}
const GameButton = ({
  children,
  color = "primary",
  onClick,
  className,
}: Props) => {
  return (
    <button className={`btn btn-${color} ${className}`} onClick={onClick}>
      {children}
    </button>
  );
};

export default GameButton;
