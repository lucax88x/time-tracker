import { ITimeTrackOutputModel } from '../../models/time-track.output-model';

export const getTimeTracks = `
  {
    timeTracks {
      id,
      when
    }
  }
`;
export interface IGetTimeTracksResponse {
  timeTracks: ITimeTrackOutputModel[];
}

export const getTimeTrackById = `
  query ($id: String!) {
    timeTrack(id: $id) {
      id,
      when
    }
  } 
`;
export interface IGetTimeTrackByIdPayload {
  id: string;
}
export interface IGetTimeTrackByIdResponse {
  timeTrack: ITimeTrackOutputModel;
}
