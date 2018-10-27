import { produce } from 'immer';

import { TimeTrackActions } from '../actions';
import {
  GET_TIME_TRACKS,
  GET_TIME_TRACKS_ERROR,
  GET_TIME_TRACKS_SUCCESS
} from '../actions/time-track';
import { ITimeTrackState } from '../states/time-track';

export const initialTimeTrackState: ITimeTrackState = {
  isTimeTracksBusy: false,
  timeTracks: []
};

const timeTrack = (
  state = initialTimeTrackState,
  action: TimeTrackActions
): ITimeTrackState =>
  produce(state, draft => {
    switch (action.type) {
      // case ADD_TIME_TRACK:
      //   draft.timeTracks.push({
      //     id: action.payload.id,
      //     when: ''
      //   });
      //   return;
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
