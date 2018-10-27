import { forEachObjIndexed } from 'ramda';

import { ITodoApi } from './apis/interfaces';
import { TodosEpic } from './epics/todos';
import { container } from './inversify.config';
import { TYPES } from './inversify.types';

describe('Inversify Container', () => {
  it('resolve ALL the types in case you forgot some', () => {
    forEachObjIndexed(
      type => expect(container.get(type)).not.toBeNull(),
      TYPES
    );
  });
  it('resolve ITodoApi', () => {
    expect(container.get<ITodoApi>(TYPES.ITodoApi)).not.toBeNull();
  });
  it('resolve TodosEpic', () => {
    expect(container.get<TodosEpic>(TYPES.TodosEpic)).not.toBeNull();
  });
});
