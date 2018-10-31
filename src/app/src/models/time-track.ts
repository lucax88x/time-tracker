import { DateTime } from 'luxon';

import { UUID } from '../code/uuid';
import { ITimeTrackInputModel } from './time-track.input-model';
import { ITimeTrackOutputModel } from './time-track.output-model';

export interface ITimeTrackModel {
  id: UUID;
  when: DateTime;
  type: TimeTrackType;
}

export enum TimeTrackType {
  IN = 0,
  OUT = 1
}

export function fromInput(input: ITimeTrackInputModel): ITimeTrackModel {
  return {
    id: new UUID(input.id),
    when: DateTime.fromISO(input.when),
    type: TimeTrackType[input.type]
  };
}

export function fromOutput(output: ITimeTrackOutputModel): ITimeTrackModel {
  return {
    id: new UUID(output.id),
    when: DateTime.fromISO(output.when),
    type: TimeTrackType[TimeTrackType[output.type]]
  };
}
