﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }


        [HttpPost("add")]

        public IActionResult Add(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Add(operationClaim);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpPost("update")]

        public IActionResult Update(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Update(operationClaim);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpPost("delete")]

        public IActionResult Delete(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Delete(operationClaim);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpGet("getlist")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetList()
        {
            var result = _operationClaimService.GetList();

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpGet("getById")]

        public IActionResult GetById(int id)
        {
            var result = _operationClaimService.GetById(id);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

    }
}
