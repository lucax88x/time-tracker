import { createSelector } from 'reselect';

import { State } from '../states/state';

const selectTodosState = (state: State) => state.todos;
export const selectTodos = createSelector(
  selectTodosState,
  todos => todos.todos
);
export const selectIsTodosBusy = createSelector(
  selectTodosState,
  todos => todos.isTodosBusy
);
