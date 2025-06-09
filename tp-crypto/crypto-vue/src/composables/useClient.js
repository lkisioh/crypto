import { useClientStore } from "@/stores/clientStore";
import { getClient } from "@/helpers/getClient";

export const useClient= async()=>{
  const clientList = await getClient();
  const clientStore = useClientStore()

  clientStore.clientData= clientList;
}

