import { produce } from 'immer';
import {
  CLOSE_MENU,
  CLOSE_TIME_TRACK_DRAWER,
  OPEN_MENU,
  OPEN_TIME_TRACK_DRAWER
} from 'src/actions/home';
import { ADD_TIME_TRACK_SUCCESS } from 'src/actions/time-track';

import { HomeActions, TimeTrackActions } from '../actions';
import { IHomeState } from '../states/home';

export const initialHomeState: IHomeState = {
  isMenuOpen: false,
  isTimeTrackDrawerOpen: false
};

const home = (
  state = initialHomeState,
  action: HomeActions | TimeTrackActions
): IHomeState =>
  produce(state, draft => {
    switch (action.type) {
      case OPEN_MENU:
        draft.isMenuOpen = true;
        return;
      case CLOSE_MENU:
        draft.isMenuOpen = false;
        return;
      case OPEN_TIME_TRACK_DRAWER:
        draft.isTimeTrackDrawerOpen = true;
        return;
      case CLOSE_TIME_TRACK_DRAWER:
      case ADD_TIME_TRACK_SUCCESS:
        draft.isTimeTrackDrawerOpen = false;
        return;
      default:
        return state;
    }
  });

export default home;
