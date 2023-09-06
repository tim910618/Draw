import exampleService from '../Services/exampleService';
import checkedValidationUtil from '../../utils/checkedValidationUtil';
import formatResponseUtil from '../../utils/formatResponseUtil';

class exampleController {
    //request response
    //req帶值
    Get = async(req, res) => {
        const response = await exampleService.Get();

        return formatResponseUtil.formatResponse(res, response);
    };

    GetQuery = async(req, res) => {
        const queryKey = ['num', 'str'];
        
        if (!checkedValidationUtil.keyChecked(queryKey, req.query))
            return formatResponseUtil.keyErrorResponse(res, '欄位格式有誤，請檢查');

        const { num, str } = req.query;
        const response = await exampleService.GetQuery(num, str);

        return formatResponseUtil.formatResponse(res, response);
    };

    Alldata = async(req, res) => {
        const response = await exampleService.Alldata();

        return formatResponseUtil.formatResponse(res, response);
    };

    Create = async(req, res) => {
        const createKey = ['name', 'content'];

        if (!checkedValidationUtil.keyChecked(createKey, req.body))
            return formatResponseUtil.keyErrorResponse(res, '欄位格式有誤，請檢查');

        const { name, content } = req.body;
        const response = await exampleService.Create(name, content);

        return formatResponseUtil.formatResponse(res, response);
    };

    Edit = async(req, res) => {
        const createKey = ['id', 'name', 'content'];

        if (!checkedValidationUtil.keyChecked(createKey, req.body))
            return formatResponseUtil.keyErrorResponse(res, '欄位格式有誤，請檢查');

        const { id, name, content } = req.body;
        const response = await exampleService.Edit(id, name, content);

        return formatResponseUtil.formatResponse(res, response);
    };

    Delete = async(req, res) => {
        const deleteKey = ['id'];

        if (!checkedValidationUtil.keyChecked(deleteKey, req.body))
            return formatResponseUtil.keyErrorResponse(res, '欄位格式有誤，請檢查');

        const { id } = req.body;
        const response = await exampleService.Delete(id);

        return formatResponseUtil.formatResponse(res, response);
    };
}

export default new exampleController();