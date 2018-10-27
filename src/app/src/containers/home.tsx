import { connect } from 'react-redux';
import { Dispatch } from 'redux';

import { HomeActions, TimeTrackActions } from '../actions';
import {
  closeMenuAction,
  closeTimeTrackDrawerAction,
  openMenuAction,
  openTimeTrackDrawerAction
} from '../actions/home';
import { addTimeTrackAction } from '../actions/time-track';
import HomeComponent, { IHomeDispatches, IHomeProps } from '../components/home';
import {
  selectIsMenuOpen,
  selectIsTimeTrackDrawerOpen
} from '../selectors/home';
import { State } from '../states/state';

const mapStateToProps = (state: State): IHomeProps => ({
  isMenuOpen: selectIsMenuOpen(state),
  isTimeTrackDrawerOpen: selectIsTimeTrackDrawerOpen(state)
});

const mapDispatchToProps = (
  dispatch: Dispatch<HomeActions | TimeTrackActions>
): IHomeDispatches => ({
  openMenu: () => dispatch(openMenuAction()),
  closeMenu: () => dispatch(closeMenuAction()),
  openTimeTrackDrawer: () => dispatch(openTimeTrackDrawerAction()),
  closeTimeTrackDrawer: () => dispatch(closeTimeTrackDrawerAction()),
  addTimeTrack: model => dispatch(addTimeTrackAction(model))
});

const Home = connect(
  mapStateToProps,
  mapDispatchToProps
)(HomeComponent);

export default Home;
