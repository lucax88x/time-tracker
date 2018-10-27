import { inject, injectable } from 'inversify';
import { Epic } from 'redux-observable';
import { of } from 'rxjs';
import { catchError, filter, map, startWith, switchMap } from 'rxjs/operators';
import { isOfType } from 'typesafe-actions';

import { TimeTrackActions } from '../actions';
import {
  ADD_TIME_TRACK,
  addTimeTrackErrorAction,
  addTimeTrackSuccessAction,
  GET_TIME_TRACKS,
  getTimeTracksAction,
  getTimeTracksErrorAction,
  getTimeTracksSuccessAction
} from '../actions/time-track';
import { ITimeTrackApi } from '../apis/interfaces';
import { TYPES } from '../inversify.types';
import { ITimeTrackState } from '../states/time-track';

@injectable()
export class TimeTrackEpic {
  public constructor(
    @inject(TYPES.ITimeTrackApi) private timeTrackApi: ITimeTrackApi
  ) {}

  public get epics() {
    return [this.addTimeTrack, this.getTimeTracks];
  }

  public addTimeTrack: Epic<
    TimeTrackActions,
    TimeTrackActions,
    ITimeTrackState
  > = action$ =>
    action$.pipe(
      filter(isOfType(ADD_TIME_TRACK)),
      switchMap(({ payload }) =>
        this.timeTrackApi.create(payload).pipe(
          map(_ => addTimeTrackSuccessAction()),
          catchError(error => of(addTimeTrackErrorAction()))
        )
      )
    );

  public getTimeTracks: Epic<
    TimeTrackActions,
    TimeTrackActions,
    ITimeTrackState
  > = action$ =>
    action$.pipe(
      startWith(getTimeTracksAction()),
      filter(isOfType(GET_TIME_TRACKS)),
      switchMap(_ =>
        this.timeTrackApi.get().pipe(
          map(items => getTimeTracksSuccessAction(items)),
          catchError(error => of(getTimeTracksErrorAction()))
        )
      )
    );
}
