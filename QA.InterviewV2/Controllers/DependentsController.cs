﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using QA.InterviewV2.Data.Entities;
using QA.InterviewV2.Data.Repositories;
using QA.InterviewV2.Models;

namespace QA.InterviewV2.Controllers
{
    [Route("api/employees/{id}/dependents")]
    public class DependentsController : ApiController
    {
        private IEmployeeRepository _employeeRepository;
        public DependentsController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IEnumerable<DependentViewModel> Get(int id)
        {
            var results = _employeeRepository.GetEmployeesWithDependentsById(id);
            return Mapper.Map<IEnumerable<DependentViewModel>>(results.Dependents);   
        }
        [HttpPost]
        public DependentViewModel Post(int id, [FromBody]DependentViewModel viewModel)
        {
            var newDependent = Mapper.Map<Dependent>(viewModel);
            _employeeRepository.AddDependent(id, newDependent);
            _employeeRepository.SaveAll();
            return Mapper.Map<DependentViewModel>(newDependent);
        }
    }
}
