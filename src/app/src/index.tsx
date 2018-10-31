import {
  createMuiTheme,
  CssBaseline,
  MuiThemeProvider
} from '@material-ui/core';
import {
  ConnectedRouter,
  connectRouter,
  routerMiddleware
} from 'connected-react-router';
import { createBrowserHistory } from 'history';
import { MuiPickersUtilsProvider } from 'material-ui-pickers';
import LuxonUtils from 'material-ui-pickers/utils/luxon-utils';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Route, Switch } from 'react-router-dom';
import { applyMiddleware, createStore } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import { createLogger } from 'redux-logger';
import { createEpicMiddleware } from 'redux-observable';

import { TimeTrackActions } from './actions';
import NotFound from './components/not-found';
import Home from './containers/home';
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

const history = createBrowserHistory();

const store = createStore(
  connectRouter(history)(rootReducer),
  composeWithDevTools(
    applyMiddleware(routerMiddleware(history), epicMiddleware, logger)
  )
);

epicMiddleware.run(epics);

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <MuiThemeProvider theme={theme}>
        <MuiPickersUtilsProvider utils={LuxonUtils}>
          <CssBaseline />
          <Switch>
            <Route exact={true} path="/" component={Home} />
            <Route component={NotFound} />
          </Switch>
        </MuiPickersUtilsProvider>
      </MuiThemeProvider>
    </ConnectedRouter>
  </Provider>,
  document.getElementById('root') as HTMLElement
);
registerServiceWorker();
