import { connect } from 'react-redux';
import { Dispatch } from 'redux';

import { TimeTrackActions } from '../actions';
import { editTimeTrackAction } from '../actions/time-track';
import TimeTrackListComponent, {
  ITimeTrackListDispatches,
  ITimeTrackListProps
} from '../components/time-track-list';
import {
  selectIsTimeTracksBusy,
  selectTimeTrack
} from '../selectors/time-track';
import { State } from '../states/state';

const mapStateToProps = (state: State): ITimeTrackListProps => ({
  isBusy: selectIsTimeTracksBusy(state),
  timeTracks: selectTimeTrack(state)
});

const mapDispatchToProps = (
  dispatch: Dispatch<TimeTrackActions>
): ITimeTrackListDispatches => ({
  editTimeTrack: (id: string) => dispatch(editTimeTrackAction(id))
});

const TimeTrackList = connect(
  mapStateToProps,
  mapDispatchToProps
)(TimeTrackListComponent);

export default TimeTrackList;
