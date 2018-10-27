import { forEachObjIndexed } from 'ramda';

import { ITimeTrackApi } from './apis/interfaces';
import { TimeTrackEpic } from './epics/time-track';
import { container } from './inversify.config';
import { TYPES } from './inversify.types';

describe('Inversify Container', () => {
  it('resolve ALL the types in case you forgot some', () => {
    forEachObjIndexed(
      type => expect(container.get(type)).not.toBeNull(),
      TYPES
    );
  });
  it('resolve ITimeTrackApi', () => {
    expect(container.get<ITimeTrackApi>(TYPES.ITimeTrackApi)).not.toBeNull();
  });
  it('resolve TimeTrackEpic', () => {
    expect(container.get<TimeTrackEpic>(TYPES.TimeTrackEpic)).not.toBeNull();
  });
});
