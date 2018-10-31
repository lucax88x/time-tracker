import { Observable } from 'rxjs';

import { ITimeTrackModel } from '../models/time-track';
import { ITimeTrackInputModel } from '../models/time-track.input-model';

export interface ITimeTrackApi {
  create(model: ITimeTrackInputModel): Observable<string>;
  get(): Observable<ITimeTrackModel[]>;
  getById(id: string): Observable<ITimeTrackModel>;
}
