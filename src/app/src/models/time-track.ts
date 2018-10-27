import { DateTime } from 'luxon';

import { TimeTrackInputModel } from './time-track.input-model';

export class TimeTrackModel {
  public id: string;
  public when: DateTime;
  public type: TimeTrackType;

  constructor(input: TimeTrackInputModel) {
    this.id = !!input.id ? input.id : '';
    this.when = DateTime.fromISO(input.when);
    this.type = TimeTrackType[input.type];
  }
}

export enum TimeTrackType {
  IN = 0,
  OUT = 1
}
