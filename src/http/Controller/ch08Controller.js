import ch08Service from "../Services/ch08Service";
import checkedValidationUtil from '../../utils/checkedValidationUtil';
import formatResponseUtil from '../../utils/formatResponseUtil';


class ch08Controller{

  //Get 函式作為路由處理程序時，當收到請求時，
  //Express會自動將 req 和 res 傳遞給 Get 函式
  Get=async(req,res)=>{
    const response=await ch08Service.Get();
    return formatResponseUtil.formatResponse(res, response); 
  }

  Create=async(req,res)=>{
    const createKey=['name','content'];
    if(!checkedValidationUtil.keyChecked(createKey,req.body)){
      return formatResponseUtil.keyErrorResponse(res,"欄位格式錯誤");
    }
    
    const {name,content} = req.body;
    const response=await ch08Service.Create(name,content);

    return formatResponseUtil.formatResponse(res,response);
  }

  Edit=async (req,res)=>{
    const editKey=['id','name','content'];
    if(!checkedValidationUtil.keyChecked(editKey,req.body)){
      return formatResponseUtil.keyErrorResponse(res,"欄位格式錯誤");
    }

    const {id,name,content}=req.body;
    const response=await ch08Service.Edit(id,name,content);

    return formatResponseUtil.formatResponse(res,response);
  }

  Delete=async(req,res)=>{
    const deleteKey=['id'];
    if(!checkedValidationUtil.keyChecked(deleteKey,req.body)){
      return formatResponseUtil.keyErrorResponse(res,"欄位格式錯誤");
    }

    const {id}=req.body;
    const response=await ch08Service.Delete(id);

    return formatResponseUtil.formatResponse(res,response);
  }
}

export default new ch08Controller();