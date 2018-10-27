import { combineEpics } from 'redux-observable';
import { container } from '../inversify.config';
import { TYPES } from '../inversify.types';

import { TodosEpic } from './todos';

const todosEpic = container.get<TodosEpic>(TYPES.TodosEpic);

const epics = combineEpics(...todosEpic.epics);

export default epics;
