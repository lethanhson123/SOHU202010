﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SOHU.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MembershipPaymentController : BaseController
    {
        private readonly IMembershipPaymentRepository _menbershipPaymentResposistory;

        public MembershipPaymentController(IMembershipPaymentRepository menbershipPaymentResposistory)
        {
            _menbershipPaymentResposistory = menbershipPaymentResposistory;
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            var model = _menbershipPaymentResposistory.GetByID(ID) ?? new MembershipPayment();
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(_menbershipPaymentResposistory.GetAllToList());
        }

        [HttpGet]
        public ActionResult<string> GetByID(int ID)
        {
            return ObjectToJson(_menbershipPaymentResposistory.GetByID(ID));
        }

        [HttpPost]
        public ActionResult<string> Create(MembershipPayment model)
        {
            Result routeResult;

            model.Initialization(InitType.Insert, RequestUserID);
            int result = _menbershipPaymentResposistory.Create(model);

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
        public ActionResult<string> Update(MembershipPayment model)
        {
            Result routeResult;

            model.Initialization(InitType.Update, RequestUserID);
            int result = _menbershipPaymentResposistory.Update(model.ID, model);

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

            int result = _menbershipPaymentResposistory.Delete(ID);

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
        public ActionResult<string> SaveChange(MembershipPayment model)
        {
            Result routeResult;
            int result = 0;

            if (model.ID > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _menbershipPaymentResposistory.Update(model.ID, model);
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
                result = _menbershipPaymentResposistory.Create(model);
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