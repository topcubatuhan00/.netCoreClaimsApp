using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperaitonClaimService? _userOperaitonClaimService;

        public UserOperationClaimsController(IUserOperaitonClaimService userOperaitonClaimService)
        {
            _userOperaitonClaimService = userOperaitonClaimService;
        }

        [HttpPost("add")]

        public IActionResult Add(UserOperationClaim userOperaitonClaim)
        {
            var result = _userOperaitonClaimService.Add(userOperaitonClaim);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpPost("update")]

        public IActionResult Update(UserOperationClaim userOperaitonClaim)
        {
            var result = _userOperaitonClaimService.Update(userOperaitonClaim);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpPost("delete")]

        public IActionResult Delete(UserOperationClaim userOperaitonClaim)
        {
            var result = _userOperaitonClaimService.Delete(userOperaitonClaim);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpGet("getlist")]

        public IActionResult GetList()
        {
            var result = _userOperaitonClaimService.GetList();

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpGet("getById")]

        public IActionResult GetById(int id)
        {
            var result = _userOperaitonClaimService.GetById(id);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }
    }
}
