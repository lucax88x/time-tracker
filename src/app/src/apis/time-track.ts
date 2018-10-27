import { injectable } from 'inversify';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { rxios } from '../code/rxios';
import { TimeTrackModel } from '../models/time-track';
import { ITimeTrackApi } from './interfaces';
import { ITimeTrackInput } from './time-track/inputs';
import { createTimeTrack } from './time-track/mutations';
import { ITimeTrackOutput } from './time-track/outputs';
import {
  getTimeTrackById,
  getTimeTracks,
  IGetTimeTrackByIdModel,
  IGetTimeTracksModel
} from './time-track/queries';

@injectable()
export class TimeTrackApi implements ITimeTrackApi {
  public getById(id: string): Observable<TimeTrackModel> {
    return rxios
      .query<IGetTimeTrackByIdModel, ITimeTrackInput>(getTimeTrackById, { id })
      .pipe(map(m => m.timeTrack));
  }
  public get(): Observable<TimeTrackModel[]> {
    return rxios
      .query<IGetTimeTracksModel>(getTimeTracks)
      .pipe(map(m => m.timeTracks));
  }
  public create(model: TimeTrackModel): Observable<string> {
    return rxios
      .mutation<TimeTrackModel, ITimeTrackOutput>(createTimeTrack, model)
      .pipe(map(output => output.id));
  }
}
