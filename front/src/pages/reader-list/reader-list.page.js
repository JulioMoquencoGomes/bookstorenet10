import React from 'react';
import { Link } from 'react-router-dom';
import readersService from '../../services/readers.service';
import './reader-list.page.css';

import { useNavigate, useParams } from "react-router-dom";

function withParams(Component) {
  return props => <Component {...props} 
    params={useParams()}
    navigate={useNavigate()}
  />;
}

class ReaderListPage extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            readers: [],
        }
    }

    componentDidMount() {
        this.loadReaders()
    }

    async loadReaders() {
        try {
            let res = await readersService.list();
            
            res.data.reader.forEach((r, i) => {
                if(r.birthday) {
                    r.birthday = r.birthday.toString().split('T')[0] ?? "";
                }
            });

            this.setState({ readers: res.data.reader })
        } 
        catch (error) {
            console.log(error);
            alert("Não foi possível listar os leitores.")
        }
    }

    render() {

        return (
            <div className="container">

                <div className="page-top">
                    <div className="page-top__title">
                        <h2>Leitores cadastrados</h2>
                    </div>
                    <div className="page-top__aside">
                        <button className="btn btn-primary" onClick={() => this.props.navigate('/reader-add')}>
                            Adicionar
                        </button>
                    </div>
                </div>

                {this.state.readers.map(reader => (
                    <Link to={"/reader-detail/" + reader.id} key={reader.id}>
                        <div className="reader-card">
                            <div className="reader-card__img">
                                <img src={reader.urlimg ?? ""} />
                            </div>
                            <div className="reader-card__text">
                                <h4>{reader.name}</h4>
                                <p>{reader.birthday}</p>
                            </div>
                        </div>
                    </Link>
                ))}

            </div>
        )
    }

}

export default withParams(ReaderListPage);