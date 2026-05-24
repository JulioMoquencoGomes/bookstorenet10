import React from 'react';
import readersService from '../../services/readers.service';
import './reader-edit.page.css';

import { useNavigate, useParams } from "react-router-dom";


function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class ReaderEditPage extends React.Component {

    constructor(props){
        super(props)

        this.state = {
            id: null,
            name : '',
            author : '',
            urlimg: ''
        }

    }

    componentDidMount(){
        const readerId = this.props.params.id ?? null;
        if(readerId) {
            this.loadReader(readerId);
        }
    }

    async loadReader(readerId){
        try {
            let res = await readersService.getOne(readerId);
            let reader = res.data.reader;
            this.setState(reader);
        } catch (error) {
            console.log(error);
            alert("Não foi possível carregar os leitores.");
        }
    }

    async sendReader(){
        
        let data = {
            name : this.state.name,
            birthday : this.state.birthday,
            urlimg: this.state.urlimg ?? ""
        }

        if(!data.name || data.name === ''){
            alert("Nome é obrigatório!")
            return;
        }

        try {
            if(this.state.id){
                data.id = this.state.id;
                await readersService.edit(data, this.state.id);
                alert("Leitor editado com sucesso!");
            }
            else{
                await readersService.create(data);
                alert("Leitor criado com sucesso!")
            }
            this.props.navigate('/reader-list');
        } 
        catch (error) {
            console.log(error);
            alert("Erro ao cadastrar o leitor.");
        }
    }

    render() {

        let title = this.state.id ? 'Editar leitor' : 'Cadastrar leitor';

        return (
            <div className="container">
                <div className="page-top">
                    <div className="page-top__title">
                        <h2>{title}</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-light" onClick={() => this.props.navigate('/reader-list') }>
                            Cancelar
                        </button>
                        <button className="btn btn-primary" onClick={() => this.sendReader()}>
                            Salvar
                        </button>
                    </div>
                </div>
                <form onSubmit={e => e.preventDefault()}>
                    <div className="form-group">
                        <label htmlFor="title">Nome</label>
                        <input
                            type="text"
                            className="form-control"
                            id="title"
                            value={this.state.name}
                            onChange={e => this.setState({ name: e.target.value })} />
                    </div>

                    <div className="form-group">
                        <label htmlFor="content">Data de Nascimento</label>
                        <textarea
                            type="date"
                            className="form-control"
                            id="content"
                            value={this.state.birthday}
                            rows={4}
                            style={{resize: 'none'}}
                            onChange={e => this.setState({ birthday: e.target.value })} />
                    </div>

                    <div className="form-group">
                        <label htmlFor="batata">Url da imagem</label>
                        <input
                            type="text"
                            className="form-control"
                            id="batata"
                            value={this.state.urlimg}
                            onChange={e => this.setState({ urlimg: e.target.value })} />
                    </div>

                </form>
            </div>
        )
    }

}

export default withParams(ReaderEditPage);