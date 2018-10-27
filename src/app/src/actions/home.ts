import { action } from 'typesafe-actions';

export const OPEN_MENU = '[HOME] Open menu';
export const CLOSE_MENU = '[HOME] Close menu';
export const OPEN_TIME_TRACK_DRAWER = '[HOME] Open Time Track drawer';
export const CLOSE_TIME_TRACK_DRAWER = '[HOME] Close Time Track drawer';

export const openMenuAction = () => action(OPEN_MENU);
export const closeMenuAction = () => action(CLOSE_MENU);
export const openTimeTrackDrawerAction = () => action(OPEN_TIME_TRACK_DRAWER);
export const closeTimeTrackDrawerAction = () => action(CLOSE_TIME_TRACK_DRAWER);
