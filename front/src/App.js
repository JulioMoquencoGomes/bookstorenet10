import React from "react";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import HomePage from './pages/home/home.page';
import BookListPage from './pages/book-list/book-list.page';
import BookDetailPage from './pages/book-detail/book-detail.page';
import BookEditPage from './pages/book-edit/book-edit.page';
 
class App extends React.Component {
  render() {
    return (
        <BrowserRouter>
          <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <span>&nbsp;</span>
            <a className="navbar-brand" href="/">BookStore</a>
            <button className="navbar-toggler" 
              type="button" 
              data-toggle="collapse" 
              data-target="#navbarMenu" 
              aria-controls="navbarMenu">
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarMenu">
              <div className="navbar-nav">
                <a href="/" className="nav-item nav-link">Home</a>
                <a href="/book-list" className="nav-item nav-link">Livros</a>
              </div>
            </div>
          </nav>

          <Routes location={this.props.location}>
            <Route path="/" exact={true} Component={HomePage}/>
            <Route path="/book-list" Component={BookListPage}/>
            <Route path="/book-detail/:id" Component={BookDetailPage}/>
            <Route path="/book-add" Component={BookEditPage}/>
            <Route path="/book-edit/:id" Component={BookEditPage}/>
          </Routes>

        </BrowserRouter>
    );
  }
}

export default App;