import React from 'react'
import './index.css';
import Contract from './Contract';

class ContractList extends React.Component {

  render() {
    let items = this.props.contracts.map((d) => <Contract key={d.id} json={d} />);

    return (
        <div>
            {items}
        </div>
    );
  }
}
export default ContractList
