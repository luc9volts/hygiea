using Akka.Actor;
using hygiea.web.Actors;
using hygiea.web.Messages;
using Microsoft.AspNetCore.Mvc;

namespace hygiea.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreauthorizationController : ControllerBase
    {
        //private readonly ILogger<PreauthorizationController> _logger;
        private readonly IActorRef _router;

        public PreauthorizationController(ActorProvider actorProvider)
        {
            _router = actorProvider.Router;
        }

        [HttpPost]
        public IActionResult PostAuthorization([FromBody] AuthMessage authrequest)
        {
            _router.Tell(authrequest);
            return Accepted(authrequest);
        }
    }
}
