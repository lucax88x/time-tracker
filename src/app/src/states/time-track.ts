import { TimeTrackModel } from '../models/time-track';

export interface ITimeTrackState {
  isTimeTracksBusy: boolean;
  timeTracks: TimeTrackModel[];
}
