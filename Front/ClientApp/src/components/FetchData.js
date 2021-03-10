import React, { Component } from 'react';
import './FetchData.css';
import { Location } from './Location';
import StarRatingComponent from 'react-star-rating-component';

export class FetchData extends Component {
    static displayName = FetchData.name;
     
    constructor(props) {
        super(props);
        this.state = { Locations: [], loading: true, weather: [], selectedLocation : null };
        this.displayOpinions = this.displayOpinions.bind(this);
    }

    componentDidMount() {
        this.populateLocationData();
    }

     renderLocationsTable(Locations) {
        return (
            <div className="corps" >
                <div className="list">
                    {Locations.map((Location, index) =>
                        <div key={Location.id} >

                            <div className="card"  >
                                <div><img src={Location.linkPicture} alt="loading img" /></div>
                                <h2 className="card-title"> {Location.name} </h2>

                                <div id="rate-button">
                                    <StarRatingComponent
                                        name="rate1"
                                        starCount={5}
                                        editing={false}
                                        value={Location.rateLocation}
                                    />
                                    <div><button id={'avis-' + Location.name} className="btn btn-primary btnAvis" onClick={() => this.displayOpinions(Location)}>Savoir plus</button></div>
                                </div>

                            </div>
                        
                        </div>
                    )}
                </div>
                {
                    this.state.selectedLocation != null ? <Location key={this.state.selectedLocation.id} element={this.state.selectedLocation}/> : null
                }
                
            </div>
            
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderLocationsTable(this.state.Locations);
           
            
        return (
            <div>
                {contents}
            </div>
        );
    }

    async populateLocationData() {
        const response = await fetch('location');
        const data = await response.json();
        const promises = [];
        data.map(elt => {
            let url = 'https://api.openweathermap.org/data/2.5/weather?q=' + elt.name + ',mar&appid=c21a75b667d6f7abb81f118dcf8d4611&units=metric';
            promises.push(fetch(url)
                .then(res => res.json())
                .then(dataw => elt.weather = dataw));
        });
        const result = await Promise.all(promises);
        this.setState({ Locations: data, loading: false });
        

        let temp = []
        for (let i = 0; i < this.state.Locations.length; ++i)
            temp.push(false);

        this.setState({ clickeds: temp });
    }

    displayOpinions(Location) {
        this.setState({ selectedLocation: Location });
    }
}