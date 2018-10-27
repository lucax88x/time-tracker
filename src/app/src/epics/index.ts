import { combineEpics } from 'redux-observable';

import { container } from '../inversify.config';
import { TYPES } from '../inversify.types';
import { TimeTrackEpic } from './time-track';

const timeTrackEpic = container.get<TimeTrackEpic>(TYPES.TimeTrackEpic);

const epics = combineEpics(...timeTrackEpic.epics);

export default epics;
