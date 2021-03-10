
import React, { Component } from 'react';
import './FetchData.css';
import StarRatingComponent from 'react-star-rating-component';
import Popover from 'react-bootstrap/Popover';
import OverlayTrigger from 'react-bootstrap/OverlayTrigger';
import Button from 'react-bootstrap/Button';





export class Location extends Component {

    constructor(props) {
        super(props);
        this.state = {
            newOpinion: '', locationOpinions: this.props.element.opinionList, rateValue: 2.5,
            popover: <Popover id="popover-basic">
                <Popover.Title as="h3">Merci de donner une note</Popover.Title>
                <Popover.Content>
                    <StarRatingComponent
                        name="rate2"
                        starCount={5}
                        onStarClick={this.onStarClick.bind(this)}
                    />
                </Popover.Content>
            </Popover>
        };
        this.buttonSubmit = React.createRef();
        this.messageEnd = React.createRef();

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);


    }

    componentDidMount() {
        this.scrollToBottom()
    }
    componentDidUpdate() {
        this.scrollToBottom()
    }
    scrollToBottom = () => {
        this.messageEnd.current.scrollIntoView({ behavior: 'smooth' })
    }

    handleChange(event) {
        this.setState({ newOpinion: event.target.value });
    }

    async handleSubmit() {

        if (this.state.newOpinion !== '') {
            let op = {
                "id": 0,
                "content": this.state.newOpinion,
                "rateOpinion": this.state.rateValue
            };

            await fetch('opinion/' + this.props.element.id,
                {
                    method: 'POST',
                    headers: {
                        'Accept': '/',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(op)
                });

            let temp = this.state.locationOpinions;
            temp.push(op);
            this.setState({ locationOpinions: temp });
            this.setState({ newOpinion: '' });

            this.buttonSubmit.current.click();
        }
        else {
            alert("Veuillez rentrer un message d'avis avant de submiter");
        }
    }

    onStarClick(nextValue, prevValue, name) {
        this.setState({ rateValue: nextValue }, () => {
            this.handleSubmit();
        });
    }


    render() {
        return (
            <div className="locationDetails">
                <img src={this.props.element.linkPicture} alt="Location s image" />
           
                <div className="nameTemp">
                    <div className="locationName">
                       <h1>{this.props.element.name}</h1>
                    </div>
                    <div className="temp"><h3>{this.props.element.weather.main.temp}&#8451;</h3></div>
                </div>


                <h2>Avis : </h2>
                <div className="Opinions">
               
                    {
                        this.state.locationOpinions.map((opinion, index) =>
                            <div key={"opn" + index} className="opinion" style={{ backgroundColor: index % 2 === 1 ? '#fff' : 'rgba(0, 0, 0, 0.05)' }}>
                                <div id="opAvis"><p>{opinion.content}</p></div>
                                <StarRatingComponent
                                    name="rate1"
                                    starCount={5}
                                    editing={false}
                                    value={opinion.rateOpinion}
                                />
                            </div>
                            )
                    }
                    <div ref={this.messageEnd}></div>
                </div>
                <div className="inputOp">

                    <input type='text' placeholder='Rédiger votre Avis' className="form-control" value={this.state.newOpinion} onChange={this.handleChange}/>
                    <OverlayTrigger trigger="click" placement="top" overlay={this.state.popover}>
                        <Button variant="success" ref={this.buttonSubmit}>Poster Avis</Button>
                    </OverlayTrigger>

                </div>
            </div>
            );
    }
}