﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceRepository _invoiceResposistory;

        public InvoiceController(IInvoiceRepository invoiceResposistory)
        {
            _invoiceResposistory = invoiceResposistory;
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            var model = _invoiceResposistory.GetByID(ID) ?? new Invoice();
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(_invoiceResposistory.GetAllToList());
        }

        [HttpGet]
        public ActionResult<string> GetByID(int ID)
        {
            return ObjectToJson(_invoiceResposistory.GetByID(ID));
        }

        [HttpPost]
        public ActionResult<string> Create(Invoice model)
        {
            Result routeResult;

            model.Initialization(InitType.Insert, RequestUserID);
            int result = _invoiceResposistory.Create(model);

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
        public ActionResult<string> Update(Invoice model)
        {
            Result routeResult; 

            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceResposistory.Update(model.ID, model);
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

            int result = _invoiceResposistory.Delete(ID);

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
        public ActionResult<string> SaveChange(Invoice model)
        {
            Result routeResult;
            int result = 0;

            if (model.ID > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _invoiceResposistory.Update(model.ID, model);

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
                result = _invoiceResposistory.Create(model);

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
    }
}