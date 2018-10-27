import React from 'react';
import { TimeTrackModel } from 'src/models/time-track';

interface ITimeTrackPropDispatches {
  onClick: (id: string) => void;
}

export class TimeTrack extends React.Component<
  TimeTrackModel & ITimeTrackPropDispatches
> {
  public render() {
    return (
      <li onClick={this.handleOnClick}>
        {this.props.type} {this.props.when}
      </li>
    );
  }

  private handleOnClick = () => {
    this.props.onClick(this.props.id);
  };
}
export default TimeTrack;
