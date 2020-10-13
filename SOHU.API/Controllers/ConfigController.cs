﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Extensions;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigController : BaseController
    {
        private readonly IConfigRepository _configResposistory;

        public ConfigController(IConfigRepository configResposistory)
        {
            _configResposistory = configResposistory;
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            var model = _configResposistory.GetByID(ID) ?? new Config();
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(_configResposistory.GetAllToList());
        }

        [HttpGet]
        public ActionResult<string> GetByID(int ID)
        {
            return ObjectToJson(_configResposistory.GetByID(ID));
        }

        [HttpPost]
        public ActionResult<string> Create(Config model)
        {
            Result routeResult;

            model.Initialization(InitType.Insert, RequestUserID);
            int result = _configResposistory.Create(model);

            if (result > 0)
            {
                routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.CreateSuccess);
            }
            else
            {
                routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.InsertError)
                        .setMessage(AppGlobal.CreateFail);
            }

            return ObjectToJson(routeResult);
        }

        [HttpPut]
        public ActionResult<string> Update(Config model)
        {
            Result routeResult;

            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.ID, model);

            if (result > 0)
            {
                routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.EditSuccess);
            }
            else
            {
                routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.EditError)
                        .setMessage(AppGlobal.EditFail);
            }

            return ObjectToJson(routeResult);
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
        {
            Result routeResult;

            int result = _configResposistory.Delete(ID);

            if (result > 0)
            {
                routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.DeleteSuccess);
            }
            else
            {
                routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.DeleteError)
                        .setMessage(AppGlobal.DeleteFail);
            }
            return ObjectToJson(routeResult);
        }

        [HttpPost]
        public ActionResult<string> SaveChange(Config model)
        {
            Result routeResult;
            int result = 0;
            if (model.ID > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _configResposistory.Update(model.ID, model);

                if (result > 0)
                {
                    routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.EditSuccess);
                }
                else
                {
                    routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.EditError)
                        .setMessage(AppGlobal.EditFail);
                }
            }
            else
            {
                model.Initialization(InitType.Insert, RequestUserID);
                result = _configResposistory.Create(model);

                if (result > 0)
                {
                    routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.CreateSuccess);
                }
                else
                {
                    routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.InsertError)
                        .setMessage(AppGlobal.CreateFail);
                }
            }
            return ObjectToJson(routeResult);
        }

        [HttpGet]
        public ActionResult<string> GetTreeMenuDataTransferByCodeToList(string Code)
        {
            var data = _configResposistory.GetByCodeToList(Code);
            var result = data.GenerateTree(item => item.ID, item => item.ParentID);
            return ObjectToJson(data.GenerateTree(item => item.ID, Item => Item.ParentID));
        }

        [HttpGet]
        public ActionResult<string> GetByCodeToList(string Code)
        {
            return ObjectToJson(_configResposistory.GetByCodeToList(Code));
        }
    }
}