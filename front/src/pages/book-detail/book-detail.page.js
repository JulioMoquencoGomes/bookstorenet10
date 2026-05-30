import React from 'react';

import booksService from '../../services/books.service';
import './book-detail.page.css';

import { useNavigate, useParams } from "react-router-dom";


function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class BookDetailPage extends React.Component {
    
    constructor(props) {
        super(props)
        this.state = {
            book: null
        }
    }

    componentDidMount() {
        const bookId = this.props.params.id;
        this.loadBook(bookId);
    }

    async loadBook(bookId) {
        try {
            let res = await booksService.getOne(bookId);
            this.setState({ book: res.data.book })
        } catch (error) {
            console.log(error);
            alert("Não foi possível carregar o livro.")
        }
    }
    
    async deleteBook(bookId) {
        if (!window.confirm("Deseja realmente excluir este livro?")) return;

        try {
            await booksService.delete(bookId)
            alert("Livro excluído com sucesso")
            this.props.navigate('/book-list');
        } catch (error) {
            console.log(error);
            alert("Não foi possível excluir o livro.")
        }

    }


    render() {

        return (
            <div className="container">

                <div className="page-top">
                    <div className="page-top__title">
                        <h2>{ this.state.book?.name ?? "Livro" }</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-light" onClick={() => this.props.navigate('/book-list') }>
                            Voltar
                        </button>
                    </div>
                </div>

                <div className="row">
                    <div className="col-6">
                        <img className="book-img" src={this.state?.book?.urlimg ?? ""} alt="image" />
                    </div>
                    <div className="col-6">
                        <div className="book-info">
                            <h4>ID</h4>
                            <p>{this.state.book?.id}</p>
                        </div>
                        <div className="book-info">
                            <h4>Nome</h4>
                            <p>{this.state.book?.name}</p>
                        </div>
                        <div className="book-info">
                            <h4>Autor</h4>
                            <p>{this.state.book?.author}</p>
                        </div>
                        <div className="btn-group" role="group" aria-label="Basic example">
                            <button
                                type="button"
                                className="btn btn-sm btn-outline-danger"
                                onClick={() => this.deleteBook(this.state.book.id)}>
                                Excluir
                            </button>
                            <button
                                type="button"
                                className="btn btn-sm btn-outline-primary"
                                onClick={() => this.props.navigate('/book-edit/' + this.state.book.id) }>
                                Editar
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        )
    }

}

export default withParams(BookDetailPage)