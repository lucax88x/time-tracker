import { combineReducers } from 'redux';

import home from './home';
import timeTrack from './time-track';

export default combineReducers({
  home,
  timeTrack
});
