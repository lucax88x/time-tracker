import { TimeTrackModel } from 'src/models/time-track';

export const getTimeTracks = `
  {
    timeTracks {
      id,
      when
    }
  }
`;
export interface IGetTimeTracksModel {
  timeTracks: TimeTrackModel[];
}

export const getTimeTrackById = `
  query ($id: String!) {
    timeTrack(id: $id) {
      id,
      when
    }
  } 
`;
export interface IGetTimeTrackByIdModel {
  timeTrack: TimeTrackModel;
}
