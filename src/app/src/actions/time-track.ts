import { action } from 'typesafe-actions';

import { UUID } from '../code/uuid';
import { ITimeTrackModel } from '../models/time-track';
import { ITimeTrackInputModel } from '../models/time-track.input-model';

export const ADD_TIME_TRACK = '[TimeTrack] Add Time Track';
export const ADD_TIME_TRACK_SUCCESS = '[TimeTrack] Add Time Track Success';
export const ADD_TIME_TRACK_ERROR = '[TimeTrack] Add Time Track Error';

export const EDIT_TIME_TRACK = '[TimeTrack] Edit Time Track';

export const GET_TIME_TRACKS = '[TimeTrack] Get Time Tracks';
export const GET_TIME_TRACKS_SUCCESS = '[TimeTrack] Get Time Tracks Success';
export const GET_TIME_TRACKS_ERROR = '[TimeTrack] Get Time Tracks Error';

export const addTimeTrackAction = (model: ITimeTrackInputModel) =>
  action(ADD_TIME_TRACK, model);
export const addTimeTrackSuccessAction = () => action(ADD_TIME_TRACK_SUCCESS);
export const addTimeTrackErrorAction = () => action(ADD_TIME_TRACK_ERROR);

export const editTimeTrackAction = (id: UUID) => action(EDIT_TIME_TRACK, id);

export const getTimeTracksAction = () => action(GET_TIME_TRACKS);
export const getTimeTracksSuccessAction = (timeTracks: ITimeTrackModel[]) =>
  action(GET_TIME_TRACKS_SUCCESS, timeTracks);
export const getTimeTracksErrorAction = () => action(GET_TIME_TRACKS_ERROR);
