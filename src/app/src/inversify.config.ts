import 'reflect-metadata';

import { Container } from 'inversify';

import { ITimeTrackApi } from './apis/interfaces';
import { TimeTrackApi } from './apis/time-track';
import { TimeTrackEpic } from './epics/time-track';
import { TYPES } from './inversify.types';

const container = new Container();

container.bind<ITimeTrackApi>(TYPES.ITimeTrackApi).to(TimeTrackApi);
container.bind<TimeTrackEpic>(TYPES.TimeTrackEpic).to(TimeTrackEpic);

export { container };
