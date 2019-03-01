import React from 'react';
import { Route, Redirect } from 'react-router-dom';

import { AUTH_MODEL } from './Auth';

export const PrivateRoute = ({ component: Component, roles, ...rest }) => (
    <Route {...rest} render={props => {
        const currentUser = AUTH_MODEL.get().userInfo;
        const token = AUTH_MODEL.get().token;
        if (!token) {
            // not logged in so redirect to login page with the return url
            return <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
        }

        // check if route is restricted by role
        if (roles && roles.indexOf(currentUser.role) === -1) {
            // role not authorised so redirect to home page
            return <Redirect to={{ pathname: '/' }} />
        }

        // authorized so return component
        return <Component {...props} />
    }} />
)