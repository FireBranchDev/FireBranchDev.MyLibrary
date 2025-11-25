using Microsoft.AspNetCore.Mvc;

namespace FireBranchDev.MyLibrary.Api.Controllers;

[Consumes("application/vnd.api+json")]
[Produces("application/vnd.api+json")]
[ApiController]
public class JsonApiControllerBase : ControllerBase
{
}
