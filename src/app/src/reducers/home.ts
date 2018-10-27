import { produce } from 'immer';
import {
  CLOSE_MENU,
  CLOSE_TIME_TRACK_DRAWER,
  OPEN_MENU,
  OPEN_TIME_TRACK_DRAWER
} from 'src/actions/home';

import { HomeActions } from '../actions';
import { IHomeState } from '../states/home';

export const initialHomeState: IHomeState = {
  isMenuOpen: false,
  isTimeTrackDrawerOpen: false
};

const home = (state = initialHomeState, action: HomeActions): IHomeState =>
  produce(state, draft => {
    switch (action.type) {
      case OPEN_MENU:
        draft.isMenuOpen = true;
        console.log(draft);
        return;
      case CLOSE_MENU:
        draft.isMenuOpen = false;
        return;
      case OPEN_TIME_TRACK_DRAWER:
        draft.isTimeTrackDrawerOpen = true;
        return;
      case CLOSE_TIME_TRACK_DRAWER:
        draft.isTimeTrackDrawerOpen = false;
        return;
      default:
        return state;
    }
  });

export default home;
