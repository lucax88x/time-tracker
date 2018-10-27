import React from 'react';

interface ITodoPropProps {
  id: number;
  completed: boolean;
  title: string;
}

interface ITodoPropDispatches {
  onClick: (id: number) => void;
}

export class Todo extends React.Component<
  ITodoPropProps & ITodoPropDispatches
> {
  public render() {
    return (
      <li
        onClick={this.handleOnClick}
        style={{
          textDecoration: this.props.completed ? 'line-through' : 'none'
        }}
      >
        {this.props.title}
      </li>
    );
  }

  private handleOnClick = () => {
    this.props.onClick(this.props.id);
  }
}
export default Todo;
