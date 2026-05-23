import axios from 'axios';

const apiUrl = "http://localhost:8080";

const booksService = {

    async list(){
        const enpoint = apiUrl + "/book"
        return axios.get(enpoint);
    },

    async getOne(bookId){
        const enpoint = apiUrl + "/book/" + bookId
        return axios.get(enpoint);
    },

    async create(data){
        const enpoint = apiUrl + "/book"
        return axios.post(enpoint, data);
    },

    async edit(data, bookId){
        const enpoint = apiUrl + "/book/" + bookId
        return axios.put(enpoint, data);
    },

    async delete(bookId){
        const enpoint = apiUrl + "/book/" + bookId
        return axios.delete(enpoint);
    },


}

export default booksService;