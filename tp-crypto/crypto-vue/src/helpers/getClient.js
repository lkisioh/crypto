import axios from 'axios';

const api_url = "https://localhost:7294/api/Client";

export const getClient= async ()=>{
  
  const response =await axios.get(api_url)
  const responseData = response.data;//.navegar en el json de la respuesta si queresmos algo específico;

  return responseData;
}
