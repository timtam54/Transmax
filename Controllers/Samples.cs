using Microsoft.AspNetCore.Mvc;
using vscode.Data;
using vscode.Models;

namespace vscode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Samples: ControllerBase
    {
        private iTrafficRepo _repo;

        public Samples(iTrafficRepo repo)
        {
            _repo=repo;
        }

        [HttpGet("GetTrafficSample")]
        public ActionResult<TrafficSample?> GetTrafficSample(string id)
        {
            
            var ret= _repo.GetSample(id) ;
            if (ret!=null)
                return Ok(ret);
            return  NotFound();
        }

        [HttpGet("GetAllLatest")]
        public ActionResult<List<TrafficSample>?> GetAllLatest()
        {
            
            var ret= _repo.GetLastForEachBot() ;
            if (ret!=null)
                return Ok(ret);
            return  NotFound();
        }

        [HttpPost]
        public ActionResult<bool> Create(TrafficSample ts)
        {
           
            var result= _repo.AddSample(ts);
            return Ok(result);  
        }
    }
}