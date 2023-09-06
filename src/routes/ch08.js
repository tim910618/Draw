import { exampleMiddleware } from "../middlewares/exampleMiddleware";
import ch08Controller from "../http/Controller/ch08Controller";

const api={
  namespace:'ch08',
  routes:[{
    RestFulMethod:'get',
    path:'/AllData',
    method:ch08Controller.Get,
    Middleware:[exampleMiddleware],
  },
  {
    //新增
    RestFulMethod:'post',
    path:'/Create',
    method:ch08Controller.Create,
    Middleware:[exampleMiddleware],
  },
  {
    //修改
    RestFulMethod:'patch',
    path:'/Edit',
    method:ch08Controller.Edit,
    Middleware:[exampleMiddleware],
  },
  {
    //刪除
    RestFulMethod:'delete',
    path:'/Delete',
    method:ch08Controller.Delete,
    Middleware:[exampleMiddleware],
  },
  ],
  
};


export default api;