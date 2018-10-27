import { map } from 'ramda';
import React from 'react';

import { TimeTrackModel } from '../models/time-track';
import TimeTrack from './time-track';

export interface ITimeTrackListProps {
  isBusy: boolean;
  timeTracks: TimeTrackModel[];
}

export interface ITimeTrackListDispatches {
  editTimeTrack: (id: string) => void;
}

export class TimeTrackListComponent extends React.Component<
  ITimeTrackListProps & ITimeTrackListDispatches
> {
  public render() {
    return (
      <div>
        <ul>
          {map(
            timeTrack => (
              <TimeTrack
                key={timeTrack.id}
                {...timeTrack}
                onClick={this.onEdit}
              />
            ),
            this.props.timeTracks
          )}
        </ul>
      </div>
    );
  }

  private onEdit = (id: string) => {
    this.props.editTimeTrack(id);
  };
}

export default TimeTrackListComponent;
