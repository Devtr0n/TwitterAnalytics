import React, { Component } from 'react';

export class Counter extends Component {
    static displayName = Counter.name;

    constructor(props) {
        super(props);
        this.state = { totalCount: 0 };
    }

    componentDidMount() {
        this.populateTotalTweetCountData();
    }

    static renderTotalCountTable(totalCount) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Total Tweet Count</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{totalCount}</td>
                    </tr>
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Counter.renderTotalCountTable(this.state.totalCount);

        return (
            <div>
                <h1 id="tabelLabel" >Total Count Of Tweets</h1>
                <p>This component demonstrates fetching the total count of all tweets from the server.</p>
                {contents}
            </div>
        );
    }

    async populateTotalTweetCountData() {
        const response = await fetch('https://localhost:7044/api/Tweet/TotalCount');
        const data = await response.json();
        this.setState({ totalCount: data, loading: false });
    }
}
