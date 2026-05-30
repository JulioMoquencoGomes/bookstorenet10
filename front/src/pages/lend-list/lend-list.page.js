import React from 'react';
import { Link } from 'react-router-dom';
import lendsService from '../../services/lends.service';
import './lend-list.page.css';

import { useNavigate, useParams } from "react-router-dom";

function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class LendListPage extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            lends: [],
        }
    }

    componentDidMount() {
        this.loadLends()
    }

    async loadLends() {
        try {
            let res = await lendsService.list();
            
            res.data.lend.forEach((r, i) => {
                if(r.deliveryDate) {
                    r.deliveryDate = r.deliveryDate.toString().split('T')[0] ?? "";
                }
                r.startDate = r.startDate.toString().split('T')[0] ?? "";
                r.endDate = r.endDate.toString().split('T')[0] ?? "";
            });

            this.setState({ lends: res.data.lend })
        } 
        catch (error) {
            console.log(error);
            alert("Não foi possível listar os empréstimos.")
        }
    }

    render() {

        return (
            <div className="container">

                <div className="page-top">
                    <div className="page-top__title">
                        <h2>Empréstimos</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-primary" onClick={() => this.props.navigate('/lend-add')}>
                            Adicionar
                        </button>
                    </div>
                </div>

                {this.state.lends.map(lend => (
                    <Link to={"/lend-detail/" + lend.id} key={lend.id}>
                        <div className="lend-card">
                            <div className="lend-card__img">
                                <img src={lend.book?.urlimg ?? "image"} />
                            </div>
                            <div className="lend-card__text">
                                <h4>{lend.reader.name}</h4>
                                <p>{lend.book.name}</p>
                                <p>{lend.startDate}{ lend.deliveryDate ? " - Já entregue" : "" }</p>
                            </div>
                        </div>
                    </Link>
                ))}

            </div>
        )
    }

}

export default withParams(LendListPage);