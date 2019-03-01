import React, { Component } from 'react';

export class Settings extends Component {
  displayName = Settings.name

  constructor(props) {
    super(props);
    this.state = { };
  }

  render() {
    return (
      <div>
        <h1>Settings</h1>
      </div>
    );
  }
}
