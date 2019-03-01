import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

export class EditUser extends Component {
    constructor(props) {
        super(props);

        this.state = {
            first_name: props.location.state.user.first_name,
            last_name: props.location.state.user.last_name,
            email: props.location.state.user.email,
            password: '',
            role: props.location.state.role,
            reRoute: false,
        };

        this.editUser.bind(this);
        this.onChange = this.onChange.bind(this);
    }

    onChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    async editUser(e) {
        e.preventDefault();

        try {
            let userInfo = {
                first_name: this.state.first_name,
                last_name: this.state.last_name,
                email: this.state.email,
                password: this.state.password,
                role: this.state.role,
            }

            const url = `api/users/edit?id=${this.props.location.state.user.id}`;
            const response = await fetch(url, {
                method: 'PUT',
                body: JSON.stringify(userInfo),
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok)
                this.setState({ reRoute: true });
        }
        catch (err) {
            console.log("err: ", err);
        }
    }

    render() {
        if (this.state.reRoute) {
            return <Redirect to="/users" />
        }
        else {
            return (
                <div>
                    <h1>Edit Employee</h1>
                    <br />
                    <form
                        id="editUser"
                        onSubmit={this.editUser.bind(this)}
                    >
                        <div className="form-group">
                            <label>First Name:</label>
                            <input
                                id="first_name"
                                type="text"
                                className="form-control"
                                value={this.state.first_name}
                                onChange={this.onChange}
                                name="first_name"
                                placeholder="First Name"
                                autoFocus
                            />
                        </div>
                        <div className="form-group">
                            <label>Last Name:</label>
                            <input
                                id="last_name"
                                type="text"
                                className="form-control"
                                value={this.state.last_name}
                                onChange={this.onChange}
                                name="last_name"
                                placeholder="Last Name"
                            />
                        </div>
                        <div className="form-group">
                            <label>Email:</label>
                            <input
                                id="email"
                                type="text"
                                className="form-control"
                                value={this.state.email}
                                onChange={this.onChange}
                                name="email"
                                placeholder="Email"
                            />
                        </div>
                        <div className="form-group">
                            <label>Password:</label>
                            <input
                                id="password"
                                type="password"
                                className="form-control"
                                value={this.state.password}
                                onChange={this.onChange}
                                name="password"
                                placeholder=""
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="roleSelect">Role:</label>
                            <select className="form-control" name="role" id="roleSelect" value={this.state.role} onChange={this.onChange}>
                                <option value={0}>User</option>
                                <option value={1}>Admin</option>
                            </select>
                        </div>
                        <button className="btn btn-primary" type="submit">
                            Submit Changes
                        </button>
                    </form>
                </div>
            )
        }
    }
}