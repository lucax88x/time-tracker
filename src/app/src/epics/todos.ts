import { inject, injectable } from 'inversify';
import { Epic } from 'redux-observable';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import { TodosActions } from '../actions';
import {
  GET_TODOS,
  getTodosErrorAction,
  getTodosSuccessAction
} from '../actions/todos';
import { ITodoApi } from '../apis/interfaces';
import { TYPES } from '../inversify.types';
import { ITodosState } from '../states/todos';

@injectable()
export class TodosEpic {
  public constructor(@inject(TYPES.ITodoApi) private todoApi: ITodoApi) {}

  public get epics() {
    return [this.getTodos];
  }

  public getTodos: Epic<TodosActions, TodosActions, ITodosState> = action$ =>
    action$.ofType(GET_TODOS).pipe(
      switchMap(_ =>
        this.todoApi.get().pipe(
          map(items => getTodosSuccessAction(items)),
          catchError(error => of(getTodosErrorAction()))
        )
      )
    );
}
