import React from 'react';
import { Link } from 'react-router-dom';
import booksService from '../../services/books.service';
import './book-list.page.css';

import { useNavigate, useParams } from "react-router-dom";

function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class BookListPage extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            books: [],
        }
    }

    componentDidMount() {
        this.loadBooks()
    }

    async loadBooks() {
        try {
            let res = await booksService.list();
            this.setState({ books: res.data.book })
        } catch (error) {
            console.log(error);
            alert("Não foi possível listar os livros.")
        }
    }

    render() {

        return (
            <div className="container">

                <div className="page-top">
                    <div className="page-top__title">
                        <h2>Livros cadastrados</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-primary" onClick={() => this.props.navigate('/book-add')}>
                            Adicionar
                        </button>
                    </div>
                </div>

                {this.state.books.map(book => (
                    <Link to={"/book-detail/" + book.id} key={book.id}>
                        <div className="book-card">
                            <div className="book-card__img">
                                <img src={book.urlimg ?? ""} />
                            </div>
                            <div className="book-card__text">
                                <h4>{book.name}</h4>
                                <p>{book.author}</p>
                            </div>
                        </div>
                    </Link>
                ))}

            </div>
        )
    }

}

export default withParams(BookListPage);