import { produce } from 'immer';

import { TimeTrackActions } from '../actions';
import {
  ADD_TIME_TRACK,
  ADD_TIME_TRACK_ERROR,
  ADD_TIME_TRACK_SUCCESS,
  GET_TIME_TRACKS,
  GET_TIME_TRACKS_ERROR,
  GET_TIME_TRACKS_SUCCESS
} from '../actions/time-track';
import { ITimeTrackState } from '../states/time-track';

export const initialTimeTrackState: ITimeTrackState = {
  isAddTimeTrackBusy: false,
  isTimeTracksBusy: false,
  timeTracks: []
};

const timeTrack = (
  state = initialTimeTrackState,
  action: TimeTrackActions
): ITimeTrackState =>
  produce(state, draft => {
    switch (action.type) {
      case ADD_TIME_TRACK:
        draft.isAddTimeTrackBusy = true;
        return;
      case ADD_TIME_TRACK_SUCCESS:
        draft.isAddTimeTrackBusy = false;
        draft.timeTracks.push(action.payload);
        return;
      case ADD_TIME_TRACK_ERROR:
        draft.isAddTimeTrackBusy = false;
        return;
      case GET_TIME_TRACKS:
        draft.isTimeTracksBusy = true;
        draft.timeTracks = [];
        return;
      case GET_TIME_TRACKS_SUCCESS:
        draft.isTimeTracksBusy = false;
        draft.timeTracks = action.payload;
        return;
      case GET_TIME_TRACKS_ERROR:
        draft.isTimeTracksBusy = false;
        return;
      default:
        return state;
    }
  });

export default timeTrack;
