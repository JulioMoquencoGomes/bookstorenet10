import React from "react";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import HomePage from './pages/home/home.page';

import BookListPage from './pages/book-list/book-list.page';
import BookDetailPage from './pages/book-detail/book-detail.page';
import BookEditPage from './pages/book-edit/book-edit.page';

import ReaderListPage from './pages/reader-list/reader-list.page';
import ReaderDetailPage from './pages/reader-detail/reader-detail.page';
import ReaderEditPage from './pages/reader-edit/reader-edit.page';
 
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
                <a href="/reader-list" className="nav-item nav-link">Leitores</a>
              </div>
            </div>
          </nav>

          <Routes location={this.props.location}>
            <Route path="/" exact={true} Component={HomePage}/>

            <Route path="/book-list" Component={BookListPage}/>
            <Route path="/book-detail/:id" Component={BookDetailPage}/>
            <Route path="/book-add" Component={BookEditPage}/>
            <Route path="/book-edit/:id" Component={BookEditPage}/>

            <Route path="/reader-list" Component={ReaderListPage}/>
            <Route path="/reader-detail/:id" Component={ReaderDetailPage}/>
            <Route path="/reader-add" Component={ReaderEditPage}/>
            <Route path="/reader-edit/:id" Component={ReaderEditPage}/>

          </Routes>

        </BrowserRouter>
    );
  }
}

export default App;