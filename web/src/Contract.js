import React from 'react';
import { Row, Col } from 'react-bootstrap';
import './index.css';

class Contract extends React.Component {
    render() {
      return (
        <div>
          <Row>
            <Col md={4}>
              <b>{this.props.json.title}</b> <br/>
              <b>Published date: </b> {this.props.json.publishedDate} <br/>
              <b>Deadline date: </b>{this.props.json.deadlineDate} <br/>
              <b>Awarded date: </b>{this.props.json.awardedDate} <br/>
            </Col>
            <Col md={8} className='text-box'>
              {this.props.json.organisationName} <br />
              {this.props.json.description}
            </Col>
          </Row>
          <hr />
        </div>
      );

    }
}
export default Contract
