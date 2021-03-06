import { RouterState } from 'connected-react-router';

import { IHomeState } from './home';
import { ITimeTrackState } from './time-track';

export class State {
  public home: IHomeState;
  public timeTrack: ITimeTrackState;
  public router: RouterState;
}
