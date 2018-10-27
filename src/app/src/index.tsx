import './index.scss';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { applyMiddleware, createStore } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import { createLogger } from 'redux-logger';
import { createEpicMiddleware } from 'redux-observable';

import { TodosActions } from './actions';
import App from './app';
import epics from './epics';
import rootReducer from './reducers';
import registerServiceWorker from './registerServiceWorker';
import { ITodosState } from './states/todos';

const logger = createLogger({
  collapsed: true
});

const epicMiddleware = createEpicMiddleware<
  TodosActions,
  TodosActions,
  ITodosState
>();

const store = createStore(
  rootReducer,
  composeWithDevTools(applyMiddleware(epicMiddleware, logger))
);

epicMiddleware.run(epics);

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('root') as HTMLElement
);
registerServiceWorker();
