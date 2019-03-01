import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import './StyleSheet.css';
import './NavMenu.css';

export class Layout extends Component {
    displayName = Layout.name

    render() {
      return (
        <div className="container-fluid">
            <NavMenu />
            <div className="row">
                <div className='col-md-2'></div>
                <div className='col-md-8'>
                    {this.props.children}
                </div>
                <div className='col-md-2'></div>
            </div>
        </div>
    );
  }
}
