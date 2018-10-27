import React, { FormEvent } from 'react';

export interface IAddTodoDispatches {
  addTodo: (value: string) => void;
}

export class AddTodo extends React.Component<IAddTodoDispatches> {
  private input: React.RefObject<HTMLInputElement> = React.createRef();

  public render() {
    return (
      <div>
        <form onSubmit={this.onSubmit}>
          <input ref={this.input} />
          <button type="submit">Add Todo</button>
        </form>
      </div>
    );
  }

  public onSubmit = (e: FormEvent) => {
    e.preventDefault();
    if (!!this.input.current) {
      this.props.addTodo(this.input.current.value);
    }
  };
}

export default AddTodo;
