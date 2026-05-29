import axios from 'axios';

const apiUrl = "http://localhost:8080/api";

const lendsService = {

    async list(){
        const enpoint = apiUrl + "/lend"
        return axios.get(enpoint);
    },

    async getOne(lendId){
        const enpoint = apiUrl + "/lend/" + lendId
        return axios.get(enpoint);
    },

    async create(data){
        const enpoint = apiUrl + "/lend"
        return axios.post(enpoint, data);
    },

    async edit(data, lendId){
        const enpoint = apiUrl + "/lend/" + lendId
        return axios.put(enpoint, data);
    },

    async delete(lendId){
        const enpoint = apiUrl + "/lend/" + lendId
        return axios.delete(enpoint);
    },


}

export default lendsService;