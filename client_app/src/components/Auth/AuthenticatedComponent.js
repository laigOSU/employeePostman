import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import { AUTH_MODEL } from '../../Shared/Auth/Auth';

class AuthenticatedComponent extends Component {
    constructor(props) {
        super(props);

        this.state = {
            user: undefined
        };
    }

    async componentDidMount() {
        const jwt = AUTH_MODEL.get().token;
        if (!jwt) {
            AUTH_MODEL.remove();
            this.props.history.push('/login');
        }

        try {
            const { userInfo } = AUTH_MODEL.get();

            this.setState({ user: userInfo });
        } catch (err) {
            AUTH_MODEL.remove();
            this.props.history.push('/login');
        }
    }

    render() {
        if (this.state.user === undefined) {
            return (
                <div><h1>Loading...</h1></div>
            );
        }

        return (
            <div>
                {this.props.children}
            </div>
        );
    }

}

export default withRouter(AuthenticatedComponent);