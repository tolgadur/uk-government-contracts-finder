import React from 'react';
import ReactDOM from 'react-dom';
import 'bootstrap/dist/css/bootstrap.css';
import './index.css';
import { FaSearch } from 'react-icons/fa';
import { Navbar, Form, Button, InputGroup} from 'react-bootstrap';
import ContractList from './ContractList';
import reportWebVitals from './reportWebVitals';


class App extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      description: '',
      fetchResult: [],
    }
  }

  _onClick() {
    fetch('/contracts/search', {
        crossDomain: true,
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({description: this.state.description})
    }).then((response) => response.json()
    ).then((json) => this.setState( {fetchResult: json.matchingContracts})
    ).catch(error => console.log(error));
  }

  render() {
    return(
      <div>
        <Navbar bg="light" expand="lg">
          <Navbar.Brand href='/'><h3>UK Contract Finder</h3></Navbar.Brand>
          <Navbar.Collapse id="basic-navbar-nav">
            <InputGroup>
              <Form.Control
                  className='inputForm'
                  type='text'
                  value={this.state.foodToCheck}
                  placeholder='Search for foods, drinks or ingredients'
                  onChange={(input) =>  this.setState({foodToCheck: input.target.value})}
                  onKeyPress={event => {if (event.key === "Enter") this._onClick()}}
              />
              <InputGroup.Append>
                <Button variant="outline-secondary" onClick={() => this._onClick()}>
                  <FaSearch />
                </Button>
              </InputGroup.Append>
            </InputGroup>
          </Navbar.Collapse>
        </Navbar>

        <div className='contracts'>
          <ContractList key={'1'} contracts={this.state.fetchResult} />
        </div>
      </div>
    );
  }
}

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

reportWebVitals();
