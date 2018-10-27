import { createSelector } from 'reselect';

import { State } from '../states/state';

const selectHomeState = (state: State) => state.home;

export const selectIsMenuOpen = createSelector(
  selectHomeState,
  home => home.isMenuOpen
);
export const selectIsTimeTrackDrawerOpen = createSelector(
  selectHomeState,
  home => home.isTimeTrackDrawerOpen
);
