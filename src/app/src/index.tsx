import './index.scss';

import {
  createMuiTheme,
  CssBaseline,
  MuiThemeProvider
} from '@material-ui/core';
import { MuiPickersUtilsProvider } from 'material-ui-pickers';
import LuxonUtils from 'material-ui-pickers/utils/luxon-utils';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { applyMiddleware, createStore } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import { createLogger } from 'redux-logger';
import { createEpicMiddleware } from 'redux-observable';

import { TimeTrackActions } from './actions';
import App from './app';
import epics from './epics';
import rootReducer from './reducers';
import registerServiceWorker from './registerServiceWorker';
import { ITimeTrackState } from './states/time-track';

const theme = createMuiTheme({
  typography: {
    useNextVariants: true
  },
  palette: {
    type: 'dark'
  }
});

const logger = createLogger({
  collapsed: true
});

const epicMiddleware = createEpicMiddleware<
  TimeTrackActions,
  TimeTrackActions,
  ITimeTrackState
>();

const store = createStore(
  rootReducer,
  composeWithDevTools(applyMiddleware(epicMiddleware, logger))
);

epicMiddleware.run(epics);

ReactDOM.render(
  <Provider store={store}>
    <MuiThemeProvider theme={theme}>
      <MuiPickersUtilsProvider utils={LuxonUtils}>
        <CssBaseline />

        <App />
      </MuiPickersUtilsProvider>
    </MuiThemeProvider>
  </Provider>,
  document.getElementById('root') as HTMLElement
);
registerServiceWorker();
