import { ref} from 'vue'
import { defineStore } from 'pinia'


export const useClientStore = defineStore('clients',()=>{

  const clientData = ref([]);
  const setClientList = (data) =>{
    clientData.value = data
  }
  return {clientData,setClientList}
});

