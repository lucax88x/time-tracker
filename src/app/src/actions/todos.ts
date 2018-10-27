import { action } from 'typesafe-actions';

import { TodoModel } from '../models/todo';

export const ADD_TODO = '[TODOS] Add Todo';
export const TOGGLE_TODO = '[TODOS] Toggle Todo';

export const GET_TODOS = '[TODOS] Get Todos';
export const GET_TODOS_SUCCESS = '[TODOS] Get Todos Success';
export const GET_TODOS_ERROR = '[TODOS] Get Todos Error';

export const addTodoAction = (id: number, title: string) =>
  action(ADD_TODO, { id, title });
export const toggleTodoAction = (id: number) => action(TOGGLE_TODO, id);

export const getTodosAction = () => action(GET_TODOS);
export const getTodosSuccessAction = (todos: TodoModel[]) =>
  action(GET_TODOS_SUCCESS, todos);
export const getTodosErrorAction = () => action(GET_TODOS_ERROR);
