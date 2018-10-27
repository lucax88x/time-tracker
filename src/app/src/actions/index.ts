import { ActionType } from 'typesafe-actions';

import * as todos from './todos';

export type TodosActions = ActionType<typeof todos>;
