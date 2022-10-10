import React, { Component } from 'react';

export class Algorithm extends Component {
    static displayName = Algorithm.name;

    constructor(props) {
        super(props);
        this.state = { toptens: [], loading: true };
        this.interval = setInterval(() => this.setState({ time: Date.now() }), 1000);
    }

    componentDidMount() {
        this.populateTopTenTweetData();
    }

    componentWillUnmount() {
        clearInterval(this.interval);
    }

    static rendertoptensTable(toptens) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Hashtag</th>
                        <th>Count</th>
                    </tr>
                </thead>
                <tbody>
                    {toptens.map(topten =>
                        <tr key={'topten_' + topten.count + topten.hashtag} >
                            <td>{topten.hashtag}</td>
                            <td>{topten.count}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Algorithm.rendertoptensTable(this.state.toptens);

        return (
            <div>
                <h1 id="tabelLabel" >Top Ten Hashtags</h1>
                <p>This component demonstrates fetching the top ten streaming hashtags from the server.</p>
                {contents}
            </div>
        );
    }

    async populateTopTenTweetData() {
        const response = await fetch('https://localhost:7044/api/Tweet/TopTenAlgorithm');
        const data = await response.json();
        this.setState({ toptens: data, loading: false });
    }
}
