import './app.scss';

import * as React from 'react';

import AddTodo from './containers/add-todo';
import TodoList from './containers/todo-list';

class App extends React.Component {
  public render() {
    return (
      <div className="app">
        <AddTodo />
        <TodoList />
      </div>
    );
  }
}

export default App;
