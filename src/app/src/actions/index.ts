import { ActionType } from 'typesafe-actions';

import * as home from './home';
import * as timeTrack from './time-track';

export type HomeActions = ActionType<typeof home>;
export type TimeTrackActions = ActionType<typeof timeTrack>;
