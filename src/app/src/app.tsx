import './assets/fonts/roboto/index.scss';

import * as React from 'react';

import Home from './containers/home';

class App extends React.Component {
  public render() {
    return (
      <div>
        <Home />
      </div>
    );
  }
}

export default App;
