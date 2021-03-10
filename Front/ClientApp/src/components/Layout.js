import React, { Component } from 'react';
import { Container } from 'reactstrap';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
            <Container>
                <h1> Welcome to Tripadvisor !</h1>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
