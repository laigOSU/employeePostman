import React, { Component } from 'react';

export class Home extends Component {
  displayName = Home.name

  render() {
    return (
      <div>
        <h1>Welcome to the Employee Recognition Portal!</h1>
         

            <p>Welcome to your new single-page application, built with:</p>
 <div class="row">
             <div class="col-md-6">
                         <a href="/users" class="btn btn-primary btn-block">View Accounts</a>
             </div>

             <div class="col-md-6">
                         <a href="/Award" class="btn btn-primary btn-block">View Awards</a>
                    </div>
             </div>
</div>
    );
  }
}
