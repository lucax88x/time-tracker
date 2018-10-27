import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';

import App from './app';
import { State } from './states/state';

it('renders without crashing', () => {
  // ARRANGE
  const initialState: State = {
    todos: {
      isTodosBusy: false,
      todos: []
    }
  };
  const store = configureStore()(initialState);
  const div = document.createElement('div');

  // ACT
  ReactDOM.render(
    <Provider store={store}>
      <App />
    </Provider>,
    div
  );

  ReactDOM.unmountComponentAtNode(div);
});
