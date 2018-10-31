import 'reflect-metadata';

import { DateTime } from 'luxon';
import { instance, mock, when } from 'ts-mockito';

import { TimeTrackActions } from '../actions';
import {
  GET_TIME_TRACKS_ERROR,
  GET_TIME_TRACKS_SUCCESS,
  getTimeTracksAction
} from '../actions/time-track';
import { ITimeTrackApi } from '../apis/interfaces';
import { TimeTrackApi } from '../apis/time-track';
import { UUID } from '../code/uuid';
import { container } from '../inversify.config';
import { TYPES } from '../inversify.types';
import { ITimeTrackModel, TimeTrackType } from '../models/time-track';
import { ITimeTrackState } from '../states/time-track';
import {
  getTestScheduler,
  toActionObservable,
  toStateObservable
} from '../test/observable';
import { TimeTrackEpic } from './time-track';

describe('TimeTrackEpic', () => {
  let timeTrackEpic: TimeTrackEpic;
  const mockedTimeTrackApi = mock(TimeTrackApi);

  beforeEach(() => {
    container.unbind(TYPES.ITimeTrackApi);
    container
      .bind<ITimeTrackApi>(TYPES.ITimeTrackApi)
      .toConstantValue(instance(mockedTimeTrackApi));
    timeTrackEpic = container.get<TimeTrackEpic>(TYPES.TimeTrackEpic);
  });

  describe('getTimeTrack', () => {
    it('fetches all timeTracks ', () => {
      getTestScheduler().run(({ hot, cold, expectObservable }) => {
        // ARRANGE
        const items: ITimeTrackModel[] = [
          {
            id: UUID.Generate(),
            when: DateTime.local(),
            type: TimeTrackType.IN
          }
        ];

        when(mockedTimeTrackApi.get()).thenReturn(cold('-a', { a: items }));

        const action$ = toActionObservable(
          hot<TimeTrackActions>('-a', {
            a: getTimeTracksAction()
          })
        );
        const state$ = toStateObservable(hot<ITimeTrackState>(''));

        // ACT
        const output$ = timeTrackEpic.getTimeTracks(action$, state$, null);

        // ASSERT
        expectObservable(output$).toBe('--a', {
          a: {
            payload: items,
            type: GET_TIME_TRACKS_SUCCESS
          }
        });
      });
    });
    it('gets error when cannot fetch ', () => {
      getTestScheduler().run(({ hot, cold, expectObservable }) => {
        // ARRANGE
        when(mockedTimeTrackApi.get()).thenReturn(cold('-#'));

        const action$ = toActionObservable(
          hot<TimeTrackActions>('-a', {
            a: getTimeTracksAction()
          })
        );
        const state$ = toStateObservable(hot<ITimeTrackState>(''));

        // ACT
        const output$ = timeTrackEpic.getTimeTracks(action$, state$, null);

        // ASSERT
        expectObservable(output$).toBe('--a', {
          a: {
            type: GET_TIME_TRACKS_ERROR
          }
        });
      });
    });
  });
});
