import axios from 'axios';
axios.interceptors.response.use(
  response => {
    return response; // אם התקבלה תשובה בהצלחה, פשוט החזרת התשובה
  },
  error => {
    console.error('An error occurred:', error); // אם קיבלת שגיאה מהשרת, רישום השגיאה ללוג
    return Promise.reject(error); // החזרת השגיאה להמשך טיפול
  }
);
//const apiUrl = "http://localhost:5082"
axios.defaults.baseURL = 'http://localhost:5082';
export default {
  
  getTasks: async () => {
    try{
      const result = await axios.get(`/items`)    
      console.log(result.data)
      return result.data;
    }
    catch(err){
      console.error('An error occurred in getTasks:', err);
    }
    
  },
 
  addTask: async(name)=>{
    
    try{
      console.log('addTask', name)
    const result = await axios.post(`/items?name=${name}`)   
    return result.data;
    }
    catch(err){
      console.error('An error occurred in setCompleted:', err);
    }
  },
  

  setCompleted: async(id, isComplete)=>{
    try{
      console.log('setCompleted', {id, isComplete});
    const result = await axios.put(`/items/${id}?isComplete=${isComplete}`)    
    return result.data;
    }
    catch(err){
      console.error('An error occurred in setCompleted:', err);
    }
  },

  deleteTask:async(id)=>{

    try{
      const result = await axios.delete(`/items/${id}`)    
    console.log('deleteTask')
    return result.data;
    }
    catch(err){
      console.error('An error occurred in deleteTask:', err);
    }
    
  }
};
