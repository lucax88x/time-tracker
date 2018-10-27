import './assets/fonts/roboto/index.scss';

import { CssBaseline } from '@material-ui/core';
import * as React from 'react';

import Home from './containers/home';

class App extends React.Component {
  public render() {
    return (
      <div>
        <CssBaseline />
        <Home />
      </div>
    );
  }
}

export default App;
