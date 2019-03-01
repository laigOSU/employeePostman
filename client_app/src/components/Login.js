import React, { Component } from 'react';
import ReactDOM from "react-dom";
import { AUTH_MODEL } from '../Shared/Auth/Auth';
import axios from 'axios';
import { ServerSide } from '../TestData/testUsers';

export class Login extends Component {
    displayName = Login.name

    constructor(props) {
        super(props);

        this.state = {
            email: '',
            password: ''
        };

        this.server = new ServerSide();

        this.change = this.change.bind(this);
        this.submit = this.submit.bind(this);
    }

    change(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    async submit(e) {
        e.preventDefault();
        const data = {
            email: this.state.email,
            password: this.state.password
        }

        try {
            //const response = await axios.post('/api/user/login', data, { headers: { 'Content-Type': 'application/json' } });
            const response = this.server.login(data)

            const { token } = response.data;
            const { user } = response.data;

            AUTH_MODEL.set(token, user);
            this.props.history.push('/');
        }
        catch (err) {
            var mySpan = document.getElementById('incorrect_info');
            mySpan.style.display = "";
        }
    }
  
render() {
    return (
      <div> {/*JSX ROOT*/}
        <div className="card">
            <article className="card-body">
                <h4 className="card-title text-center mb-4 mt-1">LOGIN</h4>

                <p className="text-success text-center">Please log in with your email and password</p>
                <form onSubmit={e => this.submit(e)}>
                    <div className="form-group">
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text"> <i className="fa fa-envelope"></i> </span>
                            </div>
                            <input name="email" className="form-control" placeholder="Email" type="email" onChange={e => this.change(e)} value={this.state.email}/>
                        </div> 
                    </div> 
                    <div className="form-group">
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text"> <i className="fa fa-lock"></i> </span>
                            </div>
                            <input className="form-control" name="password" placeholder="******" type="password" onChange={e => this.change(e)} value={this.state.password}/>
                        </div> 
                    </div> 
                    <div className="row">
                        <div className="col-md-6">
                            <div className="form-group">
                                <button type="submit" className="btn btn-primary btn-block"> Login  </button>
                            </div>
                        </div>
                        <div className="col-md-6">
                             <a href="/signup" className="btn btn-outline-primary btn-block">Sign up</a>
                        </div>
                    </div>
                    <p className="text-center"><a href="/ForgetPassword" className="btn">Forgot password?</a></p>
                    </form>

                    <span id="incorrect_info" style={{ display: "none" }}>
                        <br />
                        <span className="alert alert-danger col" role="alert">Incorrect UserName/Password</span>
                        <br /> <br />
                    </span>
            </article>
        </div> 
      </div> // End of JSX root in render
    );
  }
}
