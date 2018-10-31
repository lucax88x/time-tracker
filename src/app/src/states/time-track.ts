import { ITimeTrackModel } from '../models/time-track';

export interface ITimeTrackState {
  isTimeTracksBusy: boolean;
  timeTracks: ITimeTrackModel[];
}
