export const createTimeTrack = `
    mutation ($timeTrack: TimeTrackInput!) {
        timeTrack(timeTrack: $timeTrack) {
        id
        }
    }  
`;
