import { ITimeTrackInputModel } from 'src/models/time-track.input-model';

export const createTimeTrack = `
    mutation ($timeTrack: TimeTrackInput!) {
        timeTrack(timeTrack: $timeTrack) {
            id
        }
    }  
`;
export interface ICreateTimeTrackPayload {
  timeTrack: ITimeTrackInputModel;
}
export interface ICreateTimeTrackResponse {
  timeTrack: {
    id: string;
  };
}
