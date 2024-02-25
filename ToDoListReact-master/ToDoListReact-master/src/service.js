import axios from 'axios';

const apiUrl = "http://localhost:5082"

export default {
  getTasks: async () => {
    const result = await axios.get(`${apiUrl}/items`)    
    return result.data;
  },

  addTask: async(name)=>{
    try{
      console.log('addTask', name)
    const result = await axios.post(`${apiUrl}/items?name=${name}`)    
    return result.data;
    }
    catch(err){
      console.log(err);
    }
  },

  setCompleted: async(id, isComplete)=>{
    try{
      console.log('setCompleted', {id, isComplete});
    const result = await axios.put(`${apiUrl}/items/${id}?isComplete=${isComplete}`)    
    return result.data;
    }
    catch(err){
      console.log(err);
    }
  },

  deleteTask:async(id)=>{
    console.log('deleteTask')
    const result = await axios.delete(`${apiUrl}/items/${id}`)    
    return result.data;
  }
};
