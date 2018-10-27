import { injectable } from 'inversify';
import { Observable } from 'rxjs';

import { rxios } from '../code/rxios';
import { TodoModel } from '../models/todo';
import { ITodoApi } from './interfaces';

@injectable()
export class TodoApi implements ITodoApi {
  public get(): Observable<TodoModel[]> {
    return rxios.get<TodoModel[]>('https://jsonplaceholder.typicode.com/todos');
  }
}
