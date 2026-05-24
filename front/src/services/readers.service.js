import axios from 'axios';

const apiUrl = "http://localhost:8080/api";

const readersService = {

    async list(){
        const enpoint = apiUrl + "/reader"
        return axios.get(enpoint);
    },

    async getOne(readerId){
        const enpoint = apiUrl + "/reader/" + readerId
        return axios.get(enpoint);
    },

    async create(data){
        const enpoint = apiUrl + "/reader"
        return axios.post(enpoint, data);
    },

    async edit(data, readerId){
        const enpoint = apiUrl + "/reader/" + readerId
        return axios.put(enpoint, data);
    },

    async delete(readerId){
        const enpoint = apiUrl + "/reader/" + readerId
        return axios.delete(enpoint);
    },


}

export default readersService;