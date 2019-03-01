import React from "react";
import { TableButtons } from '../../Shared/TableButtons';

const User = props => {
    return (
        <tr key={props.userInfo.id}>
            <td>{props.userInfo.first_name}</td>
            <td>{props.userInfo.last_name}</td>
            <td>{props.userInfo.email}</td>
            <TableButtons
                handleDelete={props.handleDelete}
                handleEdit={props.handleEdit}
                userInfo={props.userInfo}
            />
        </tr>
    );
}

export { User };