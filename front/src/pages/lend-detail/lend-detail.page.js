import React from 'react';

import lendsService from '../../services/lends.service';
import './lend-detail.page.css';

import { useNavigate, useParams } from "react-router-dom";


function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class LendDetailPage extends React.Component {
    
    constructor(props) {
        super(props)
        this.state = {
            lend: null
        }
    }

    componentDidMount() {
        const lendId = this.props.params.id;
        this.loadLend(lendId);
    }

    async loadLend(lendId) {
        try {
            let res = await lendsService.getOne(lendId);

            if(res.data.lend.deliveryDate) {
                res.data.lend.deliveryDate = 
                    res.data.lend.deliveryDate.toString().split('T')[0] ?? "";
            }

            res.data.lend.startDate = 
                    res.data.lend.startDate.toString().split('T')[0] ?? "";
            
            res.data.lend.endDate = 
                    res.data.lend.endDate.toString().split('T')[0] ?? "";

            this.setState({ lend: res.data.lend })
        } 
        catch (error) {
            console.log(error);
            alert("Não foi possível carregar o empréstimo.")
        }
    }
    
    async deleteLend(lendId) {
        if (!window.confirm("Deseja realmente excluir?")) return;

        try {
            await lendsService.delete(lendId)
            alert("Empréstimo excluído com sucesso")
            this.props.navigate('/lend-list');
        } catch (error) {
            console.log(error);
            alert("Não foi possível excluir o empréstimo.")
        }

    }


    render() {

        return (
            <div className="container">

                <div className="page-top">
                    <div className="page-top__title">
                        <h2>{ this.state.lend?.name ?? "Empréstimo" }</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-light" onClick={() => this.props.navigate('/lend-list') }>
                            Voltar
                        </button>
                    </div>
                </div>

                <div className="row">
                    <div className="col-6">
                        <img className="lend-img" src={this.state?.lend?.book?.urlimg ?? ""} alt="image" />
                    </div>
                    <div className="col-6">
                        <div className="lend-info">
                            <h4>ID</h4>
                            <p>{this.state.lend?.id}</p>
                        </div>
                        <div className="lend-info">
                            <h4>Nome</h4>
                            <p>{this.state.lend?.reader?.name}</p>
                        </div>
                        <div className="lend-info">
                            <h4>Livro</h4>
                            <p>{this.state.lend?.book?.name}</p>
                        </div>
                        <div className="lend-info">
                            <h4>Dia do empréstimo</h4>
                            <p>{this.state.lend?.startDate}</p>
                        </div>
                        <div className="lend-info">
                            <h4>Termina o empréstimo</h4>
                            <p>{this.state.lend?.endDate}</p>
                        </div>
                        <div className="lend-info">
                            <h4>Dia da entrega:</h4>
                            <p>{this.state.lend?.deliveryDate}</p>
                        </div>
                        <div className="btn-group" role="group" aria-label="Basic example">
                            <button
                                type="button"
                                className="btn btn-sm btn-outline-danger"
                                onClick={() => this.deleteLend(this.state.lend.id)}>
                                Excluir
                            </button>
                            <button
                                type="button"
                                className="btn btn-sm btn-outline-primary"
                                onClick={() => this.props.navigate('/lend-edit/' + this.state.lend.id) }>
                                Editar
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        )
    }

}

export default withParams(LendDetailPage)