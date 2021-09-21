using Akka.Actor;
using hygiea.web.Actors;
using hygiea.web.Messages;
using Microsoft.AspNetCore.Mvc;

namespace hygiea.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IActorRef _router;

        public ServiceController(ActorProvider actorProvider) => _router = actorProvider.Router;

        [HttpPost]
        public IActionResult PostServiceRequest([FromBody] ServiceRequest service)
        {
            _router.Tell(service);
            return Accepted();
        }
    }
}
