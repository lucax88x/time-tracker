using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TimeTracker.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public async Task<bool> Get()
        {
            return await Task.FromResult(true);
        }
    }
}