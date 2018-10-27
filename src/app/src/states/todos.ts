import { TodoModel } from '../models/todo';

export interface ITodosState {
  isTodosBusy: boolean;
  todos: TodoModel[];
}
