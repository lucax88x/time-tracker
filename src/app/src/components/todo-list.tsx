import { map } from 'ramda';
import React from 'react';

import { TodoModel } from '../models/todo';
import Todo from './todo';

export interface ITodoListProps {
  isBusy: boolean;
  todos: TodoModel[];
}

export interface ITodoListDispatches {
  toggleTodo: (id: number) => void;
  getTodos: () => void;
}

export class TodoList extends React.Component<
  ITodoListProps & ITodoListDispatches
> {
  public render() {
    return (
      <div>
        <button onClick={this.onRefresh}>Refresh</button>
        <p>IsBusy: {this.props.isBusy.toString()}</p>
        <ul>
          {map(
            todo => (
              <Todo key={todo.id} {...todo} onClick={this.onToggle} />
            ),
            this.props.todos
          )}
        </ul>
      </div>
    );
  }

  private onRefresh = () => {
    this.props.getTodos();
  };

  private onToggle = (id: number) => {
    this.props.toggleTodo(id);
  };
}

export default TodoList;
