import React from 'react';

import lendsService from '../../services/lends.service';
import booksService from '../../services/books.service';
import readersService from '../../services/readers.service';

import './lend-edit.page.css';

import { useNavigate, useParams } from "react-router-dom";


function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class LendEditPage extends React.Component {

    constructor(props){
        super(props)

        this.state = {
            id: null,

            bookId : null,
            book: null,

            readerId : null,
            reader: null,

            startDate: '',
            endDate: '',
            deliveryDate: null,

            books: [],
            readers: []
        }

    }

    componentDidMount(){
        const lendId = this.props.params.id ?? null;
        if(lendId) {
            this.loadLend(lendId);
        }
        else {
            this.loadBooks();
            this.loadReaders();
        }
    }

    async loadLend(lendId){
        try {
            let res = await lendsService.getOne(lendId);
            let lend = res.data.lend;

            if(lend.deliveryDate) {
                lend.deliveryDate = lend.deliveryDate.toString().split('T')[0] ?? "";
            }

            lend.startDate = lend.startDate.toString().split('T')[0] ?? "";
            lend.endDate = lend.endDate.toString().split('T')[0] ?? "";

            this.setState(lend);

            this.loadBooks();
            this.loadReaders().then(() => {

                setTimeout(function(){
                    if(lend) {
                        document.getElementById("selectBookId").value = lend.bookId;
                        document.getElementById("selectReaderId").value = lend.readerId;
                    }
                }, 1000);                

            });

        }
        catch (error) {
            alert("Não foi possível carregar o empréstimo.");
        }
    }

    async loadBooks() {
        try {
            let res = await booksService.list();
            this.setState({ books: res.data.book });
            if(this.state.books.length > 0) {
                this.state.bookId = this.state.books[0].id;
            }
        } catch (error) {
            console.log(error);
        }
    }

    async loadReaders() {
        try {
            let res = await readersService.list();
            
            res.data.reader.forEach((r, i) => {
                if(r.birthday) {
                    r.birthday = r.birthday.toString().split('T')[0] ?? "";
                }
            });

            this.setState({ readers: res.data.reader });
            if(this.state.readers.length > 0) {
                this.state.readerId = this.state.readers[0].id;
            }
        } 
        catch (error) {
            console.log(error);
        }
    }

    async sendLend(){
        let data = {
            bookId : this.state.bookId,
            readerId : this.state.readerId,
            startDate: this.state.startDate,
            endDate: this.state.endDate
        }

        if(this.state.startDate) {
            const startDate = new Date(this.state.startDate);
            data.startDate = startDate.toISOString();
        }

        if(this.state.endDate) {
            const endDate = new Date(this.state.endDate);
            data.endDate = endDate.toISOString();
        }

        if(this.state.deliveryDate) {
            const deliveryDate = new Date(this.state.deliveryDate);
            data.deliveryDate = deliveryDate.toISOString();
        }

        try {
            if(this.state.id){
                data.id = this.state.id;
                var result = await lendsService.edit(data, this.state.id);
                messageAfterSave(result);
            }
            else{
                var result = await lendsService.create(data);
                messageAfterSave(result);
            }
            this.props.navigate('/lend-list');
        } 
        catch (error) {
            alert("Erro ao cadastrar o empréstimo.");
        }
    }

    messageAfterSave(result){
        if(result) {
            alert("Empréstimo salvo com sucesso!");
        }
        else {
            alert("Falha ao salvar");
        }
    }

    render() {

        let title = this.state.id ? 'Editar empréstimo' : 'Realizar empréstimo';

        return (
            <div className="container">
                <div className="page-top">
                    <div className="page-top__title">
                        <h2>{title}</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-light" onClick={() => this.props.navigate('/lend-list') }>
                            Cancelar
                        </button>
                        <button className="btn btn-primary" onClick={() => this.sendLend()}>
                            Salvar
                        </button>
                    </div>
                </div>

                <form onSubmit={e => e.preventDefault()}>

                    <div className="lend-card-table">

                        <div className="lend-card-column">
                            <label htmlFor="title">Livro</label>

                            <select id="selectBookId" className="form-control"
                            onChange={e => this.setState({ bookId: e.target.value })}>
                            {
                                this.state.books.map(book => (
                                    <option value={ book.id }>{book.name}</option>
                                ))
                            }
                            </select>
                        </div>

                        <div className="lend-card-column">
                            <label htmlFor="title">Leitor</label>

                            <select id="selectReaderId" className="form-control"
                            onChange={e => this.setState({ readerId: e.target.value })}>
                            {
                                this.state.readers.map(reader => (
                                    <option value={ reader.id }>{reader.name}</option>
                                ))
                            }
                            </select>
                        </div>

                    </div>


                    <div className="lend-card-table">
                        <div className="lend-card-column">
                            <label>Dia do empréstimo</label>
                            <input
                                type="date"
                                className="form-control"
                                value={this.state.startDate}
                                onChange={e => this.setState({ startDate: e.target.value })} />
                        </div>

                        <div className="lend-card-column">
                            <label>Fim do empréstimo</label>
                            <input
                                type="date"
                                className="form-control"
                                value={this.state.endDate}
                                onChange={e => this.setState({ endDate: e.target.value })} />
                        </div>
                    </div>

                    <div className="lend-card-table">
                        <div className='lend-card-column'>
                            <label>Dia da entrega</label>
                            <input
                                type="date"
                                className="form-control"
                                value={this.state.deliveryDate}
                                onChange={e => this.setState({ deliveryDate: e.target.value })} />
                        </div>
                    </div>

                </form>
            </div>
        )
    }

}

export default withParams(LendEditPage);