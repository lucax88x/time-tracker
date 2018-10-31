import { ListItem, ListItemText } from '@material-ui/core';
import { DateTime } from 'luxon';
import React from 'react';

import { UUID } from '../code/uuid';
import { ITimeTrackModel } from '../models/time-track';

interface ITimeTrackPropDispatches {
  onClick: (id: UUID) => void;
}

export class TimeTrack extends React.Component<
  ITimeTrackModel & ITimeTrackPropDispatches
> {
  public render() {
    return (
      <ListItem onClick={this.handleOnClick}>
        <ListItemText
          primary={this.props.when.toLocaleString(DateTime.DATE_HUGE)}
          secondary={this.props.when.toLocaleString(
            DateTime.TIME_24_WITH_SECONDS
          )}
        />
      </ListItem>
    );
  }

  private handleOnClick = () => {
    if (!this.props.id.isEmpty) {
      this.props.onClick(this.props.id);
    }
  };
}
export default TimeTrack;
