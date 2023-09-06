import ch08Controller from "../Controller/ch08Controller";
import ch08Repository from "../repositories/ch08Repository";

class ch08Service{
  Get=async()=>{
    try
    {
      const result=await ch08Repository.Get();
      if(!result)
      {
        return {
          status:404,
          data:null,
        };
      }
      else if(typeof result ==="string")
      {
        return {
          status:500,
          data:result,
        };
      }
      else if(typeof result ==="object")
      {
        return {
          status:200,
          data:result,
        };
      }
    }
    catch(err)
    {
      return {
        status:500,
        message:err.message,
      };
    }
  };

  Create= async(name,content)=>{
    try{
      const result=await ch08Repository.Create(name,content);
      if(!result){
        return {
          status:404,
          data:null,
        };
      }
      else if(typeof result === "string"){
        return {
          status:500,
          data:result,
        };
      }
      else if(typeof result ==="boolean"){
        return{
          status:200,
          data:result,
        };
      }
    }
    catch(err){
      return{
        status:500,
        message:err.message,
      };
    }
  };

  Edit=async(id,name,content)=>{
    try{
      const result=await ch08Repository.Edit(id,name,content);
      if(!result){
        return{
          status:404,
          data:null,
        };
      }
      else if(typeof result === "string"){
        return{
          status:500,
          data:result,
        };
      }
      else if(typeof result === "boolean"){
        return{
          status:200,
          data:result,
        };
      }
    }
    catch(err){
      return{
        status:500,
        message:err.message,
      };
    }
  };

  Delete=async(id)=>{
    try{
      const result=await ch08Repository.Delete(id);
      if(!result){
        return {
          status:404,
          data:null,
        };
      }
      else if(typeof result === "string"){
        return {
          status:500,
          data:result,
        };
      }
      else if(typeof result === "boolean"){
        return {
          status:200,
          data:result,
        };
      }
    }
    catch(err){
      return {
        status:500,
        message:err.message,
      };
    }
  }
}

export default new ch08Service();