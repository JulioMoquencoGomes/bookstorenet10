import React from 'react';

import readersService from '../../services/readers.service';
import './reader-detail.page.css';

import { useNavigate, useParams } from "react-router-dom";


function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class ReaderDetailPage extends React.Component {
    
    constructor(props) {
        super(props)
        this.state = {
            reader: null
        }
    }

    componentDidMount() {
        const readerId = this.props.params.id;
        this.loadReader(readerId);
    }

    async loadReader(readerId) {
        try {
            let res = await readersService.getOne(readerId);

            res.data.reader.birthday = 
                res.data.reader.birthday.toString().split('T')[0] ?? "";

            this.setState({ reader: res.data.reader })
        } 
        catch (error) {
            console.log(error);
            alert("Não foi possível carregar o Leitor.")
        }
    }
    
    async deleteReader(readerId) {
        if (!window.confirm("Deseja realmente excluir este leitor cadastrado?")) return;

        try {
            await readersService.delete(readerId)
            alert("Leitor excluído com sucesso")
            this.props.navigate('/reader-list');
        } catch (error) {
            console.log(error);
            alert("Não foi possível excluir o leitor.")
        }

    }


    render() {

        return (
            <div className="container">

                <div className="page-top">
                    <div className="page-top__title">
                        <h2>{ this.state.reader?.name ?? "Livro" }</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-light" onClick={() => this.props.navigate('/reader-list') }>
                            Voltar
                        </button>
                    </div>
                </div>

                <div className="row">
                    <div className="col-6">
                        <img className="reader-img" src={this.state?.reader?.urlimg ?? ""} alt="image" />
                    </div>
                    <div className="col-6">
                        <div className="reader-info">
                            <h4>ID</h4>
                            <p>{this.state.reader?.id}</p>
                        </div>
                        <div className="reader-info">
                            <h4>Nome</h4>
                            <p>{this.state.reader?.name}</p>
                        </div>
                        <div className="reader-info">
                            <h4>Data de Nascimento</h4>
                            <p>{this.state.reader?.birthday}</p>
                        </div>
                        <div className="btn-group" role="group" aria-label="Basic example">
                            <button
                                type="button"
                                className="btn btn-sm btn-outline-danger"
                                onClick={() => this.deleteReader(this.state.reader.id)}>
                                Excluir
                            </button>
                            <button
                                type="button"
                                className="btn btn-sm btn-outline-primary"
                                onClick={() => this.props.navigate('/reader-edit/' + this.state.reader.id) }>
                                Editar
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        )
    }

}

export default withParams(ReaderDetailPage)