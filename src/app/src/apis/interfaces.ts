import { Observable } from 'rxjs';

import { TimeTrackModel } from '../models/time-track';

export interface ITimeTrackApi {
  create(model: TimeTrackModel): Observable<string>;
  get(): Observable<TimeTrackModel[]>;
  getById(id: string): Observable<TimeTrackModel>;
}
