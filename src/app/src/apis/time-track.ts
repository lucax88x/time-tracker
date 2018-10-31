import { injectable } from 'inversify';
import { map as _map } from 'ramda';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { rxios } from '../code/rxios';
import { fromOutput, ITimeTrackModel } from '../models/time-track';
import { ITimeTrackInputModel } from '../models/time-track.input-model';
import { ITimeTrackApi } from './interfaces';
import {
  createTimeTrack,
  ICreateTimeTrackPayload,
  ICreateTimeTrackResponse
} from './time-track/mutations';
import {
  getTimeTrackById,
  getTimeTracks,
  IGetTimeTrackByIdPayload,
  IGetTimeTrackByIdResponse,
  IGetTimeTracksResponse
} from './time-track/queries';

@injectable()
export class TimeTrackApi implements ITimeTrackApi {
  public getById(id: string): Observable<ITimeTrackModel> {
    return rxios
      .query<IGetTimeTrackByIdResponse, IGetTimeTrackByIdPayload>(
        getTimeTrackById,
        { id }
      )
      .pipe(
        map(m => m.timeTrack),
        map(t => fromOutput(t))
      );
  }
  public get(): Observable<ITimeTrackModel[]> {
    return rxios.query<IGetTimeTracksResponse>(getTimeTracks).pipe(
      map(m => m.timeTracks),
      map(t => _map(fromOutput, t))
    );
  }
  public create(model: ITimeTrackInputModel): Observable<string> {
    return rxios
      .mutation<ICreateTimeTrackResponse, ICreateTimeTrackPayload>(
        createTimeTrack,
        {
          timeTrack: model
        }
      )
      .pipe(map(output => output.id));
  }
}
