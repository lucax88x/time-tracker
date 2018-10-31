import { ITimeTrackModel } from '../models/time-track';

export interface ITimeTrackState {
  isAddTimeTrackBusy: boolean;
  isTimeTracksBusy: boolean;
  timeTracks: ITimeTrackModel[];
}
