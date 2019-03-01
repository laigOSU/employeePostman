import React, { Component } from 'react';
import ReactDOM from "react-dom";


export class ForgetPassword extends Component {
  displayName = ForgetPassword.name
  
render() {
    return (

      <div> {/*JSX ROOT*/}
        
        <div class="card">
            <article class="card-body">
                <h4 class="card-title text-center mb-4 mt-1">RECOVER PASSWORD</h4>

                <p class="text-success text-center">Please enter your email</p>
                <form action="/login">
                <div class="form-group">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text"> <i class="fa fa-envelope"></i> </span>
                     </div>
                    <input name="" class="form-control" placeholder="Email" type="email"/>
                </div> 
                </div> 

            <div class="row">
                    <div class = "col-md-6">

                        <div class="form-group">
                        <button type="submit" class="btn btn-primary btn-block"> Send Recovery Password  </button>
                        </div>
                    </div>


                    </div>


                </form>
            </article>

            </div> 
      </div> // End of JSX root in render
    );
  }
}