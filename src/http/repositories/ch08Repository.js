import dbWrapper from '../../wrappers/dbWrapper';

class ch08Repository{
  Get=async ()=>{
    try{
      const sql=`SELECT * FROM "user";`;
      return await dbWrapper.queryResult(sql,null);
    }catch(err){
      return err.message;
    };
  }

  Create=async (name,content)=>{
    try{
      const data =[name,content];
      const sql=`INSERT INTO public."user"("name","content","create_time") VALUES ($1,$2,NOW())`;
      return await dbWrapper.query(sql,data);
    }
    catch(err){
      return err.message;
    };
  }

  Edit=async (id,name,content)=>{
    try{
      const data=[id,name,content];
      const sql=`UPDATE public."user" SET "name"=$2,"content"=$3,"create_time"=NOW() WHERE id=$1`;
      return await dbWrapper.query(sql,data);
    }
    catch(err){
      return err.message;
    }
  }

  Delete=async(id)=>{
    try{
      const data=[id];
      const sql=`DELETE FROM public."user" WHERE id=$1`;
      return await dbWrapper.query(sql,data);
    }
    catch(err){
      return err.message;
    }
  }
}

export default new ch08Repository();