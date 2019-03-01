import React, { Component } from 'react';

export class Award extends Component {
    displayName = Award.name

    constructor(props) {
        super(props);
        this.state = { awards: [], loading: true };
    }

    async componentDidMount() {
        const response = await fetch('api/SampleData/Nominated');
        const data1 = await response.json();

        console.log("DATA: ", data1)

        this.setState({ awards: data1, loading: false });
    }

    static renderNominated(awards) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Sender id</th>
                        <th>Recipient id</th>
                        <th>Type</th>
                        <th>Time</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    {awards.map(aw =>
                        <tr key={aw.id}>
                            <td>{aw.sender_user_id}</td>
                            <td>{aw.recipient_user_id}</td>
                            <td>{aw.type}</td>
                            <td>{aw.time}</td>
                            <td>{aw.date}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents1 = this.state.loading
            ? <p><em>Loading...</em></p>
            : Award.renderNominated(this.state.awards);

        return (
            <div>
                <h1>List of award infomation</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents1}
            </div>
        );
    }
}
