import { createSelector } from 'reselect';

import { State } from '../states/state';

const selectTimeTrackState = (state: State) => state.timeTrack;
export const selectTimeTrack = createSelector(
  selectTimeTrackState,
  timeTrack => timeTrack.timeTracks
);
export const selectIsTimeTracksBusy = createSelector(
  selectTimeTrackState,
  timeTrack => timeTrack.isTimeTracksBusy
);
