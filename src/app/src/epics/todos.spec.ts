import 'reflect-metadata';

import { TodosActions } from 'src/actions';
import {
  GET_TODOS_ERROR,
  GET_TODOS_SUCCESS,
  getTodosAction
} from 'src/actions/todos';
import { ITodoApi } from 'src/apis/interfaces';
import { TodoModel } from 'src/models/todo';
import { ITodosState } from 'src/states/todos';
import { instance, mock, when } from 'ts-mockito';

import { TodoApi } from '../apis/todos';
import { container } from '../inversify.config';
import { TYPES } from '../inversify.types';
import {
  getTestScheduler,
  toActionObservable,
  toStateObservable
} from '../test/observable';
import { TodosEpic } from './todos';

describe('TodosEpic', () => {
  let todosEpic: TodosEpic;
  const mockedTodosApi = mock(TodoApi);

  beforeEach(() => {
    container.unbind(TYPES.ITodoApi);
    container
      .bind<ITodoApi>(TYPES.ITodoApi)
      .toConstantValue(instance(mockedTodosApi));
    todosEpic = container.get<TodosEpic>(TYPES.TodosEpic);
  });

  describe('getTodos', () => {
    it('fetches all todos ', () => {
      getTestScheduler().run(({ hot, cold, expectObservable }) => {
        // ARRANGE
        const items: TodoModel[] = [{ id: 1, completed: false, title: 'test' }];

        when(mockedTodosApi.get()).thenReturn(cold('-a', { a: items }));

        const action$ = toActionObservable(
          hot<TodosActions>('-a', {
            a: getTodosAction()
          })
        );
        const state$ = toStateObservable(hot<ITodosState>(''));

        // ACT
        const output$ = todosEpic.getTodos(action$, state$, null);

        // ASSERT
        expectObservable(output$).toBe('--a', {
          a: {
            payload: items,
            type: GET_TODOS_SUCCESS
          }
        });
      });
    });
    it('gets error when cannot fetch ', () => {
      getTestScheduler().run(({ hot, cold, expectObservable }) => {
        // ARRANGE
        const items: TodoModel[] = [{ id: 1, completed: false, title: 'test' }];

        when(mockedTodosApi.get()).thenReturn(cold('-#', { a: items }));

        const action$ = toActionObservable(
          hot<TodosActions>('-a', {
            a: getTodosAction()
          })
        );
        const state$ = toStateObservable(hot<ITodosState>(''));

        // ACT
        const output$ = todosEpic.getTodos(action$, state$, null);

        // ASSERT
        expectObservable(output$).toBe('--a', {
          a: {
            type: GET_TODOS_ERROR
          }
        });
      });
    });
  });
});
