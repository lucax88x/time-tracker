import { List } from '@material-ui/core';
import { map } from 'ramda';
import React from 'react';

import { UUID } from '../code/uuid';
import { ITimeTrackModel } from '../models/time-track';
import TimeTrack from './time-track';

export interface ITimeTrackListProps {
  isBusy: boolean;
  timeTracks: ITimeTrackModel[];
}

export interface ITimeTrackListDispatches {
  editTimeTrack: (id: UUID) => void;
}

export class TimeTrackListComponent extends React.Component<
  ITimeTrackListProps & ITimeTrackListDispatches
> {
  public render() {
    return (
      <div>
        <List>
          {map(
            timeTrack => (
              <TimeTrack
                key={timeTrack.id.toString()}
                {...timeTrack}
                onClick={this.onEdit}
              />
            ),
            this.props.timeTracks
          )}
        </List>
        <ul />
      </div>
    );
  }

  private onEdit = (id: UUID) => {
    this.props.editTimeTrack(id);
  };
}

export default TimeTrackListComponent;
