import 'reflect-metadata';

import { Container } from 'inversify';

import { ITodoApi } from './apis/interfaces';
import { TodoApi } from './apis/todos';
import { TodosEpic } from './epics/todos';
import { TYPES } from './inversify.types';

const container = new Container();

container.bind<ITodoApi>(TYPES.ITodoApi).to(TodoApi);
container.bind<TodosEpic>(TYPES.TodosEpic).to(TodosEpic);

export { container };
