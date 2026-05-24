import React from 'react';
import booksService from '../../services/books.service';
import './book-edit.page.css';

import { useNavigate, useParams } from "react-router-dom";


function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class BookEditPage extends React.Component {

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
        const bookId = this.props.params.id ?? null;
        if(bookId) {
            this.loadBook(bookId);
        }
    }

    async loadBook(bookId){
        try {
            let res = await booksService.getOne(bookId);
            let book = res.data.book;
            this.setState(book);
        } catch (error) {
            console.log(error);
            alert("Não foi possível carregar book.");
        }
    }

    async sendBook(){
        
        let data = {
            name : this.state.name,
            author : this.state.author,
            urlimg: this.state.urlimg ?? ""
        }

        if(!data.name || data.name === ''){
            alert("Nome é obrigatório!")
            return;
        }
        if(!data.author || data.author === ''){
            alert("Autor é obrigatório!")
            return;
        }

        try {
            if(this.state.id){
                data.id = this.state.id;
                await booksService.edit(data, this.state.id);
                alert("Livro editado com sucesso!");
            }
            else{
                await booksService.create(data);
                alert("Livro criado com sucesso!")
            }
            this.props.navigate('/book-list');
        } 
        catch (error) {
            console.log(error);
            alert("Erro ao criar o livro.");
        }
    }

    render() {

        let title = this.state.id ? 'Editar livro' : 'Cadastrar livro';

        return (
            <div className="container">
                <div className="page-top">
                    <div className="page-top__title">
                        <h2>{title}</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-light" onClick={() => this.props.navigate('/book-list') }>
                            Cancelar
                        </button>
                        <button className="btn btn-primary" onClick={() => this.sendBook()}>
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
                        <label htmlFor="content">Autor</label>
                        <textarea
                            type="text"
                            className="form-control"
                            id="content"
                            value={this.state.author}
                            rows={4}
                            style={{resize: 'none'}}
                            onChange={e => this.setState({ author: e.target.value })} />
                    </div>

                    <div className="form-group">
                        <label>Url da imagem</label>
                        <input
                            type="text"
                            className="form-control"
                            value={this.state.urlimg}
                            onChange={e => this.setState({ urlimg: e.target.value })} />
                    </div>

                </form>
            </div>
        )
    }

}

export default withParams(BookEditPage);