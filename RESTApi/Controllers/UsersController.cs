using Microsoft.AspNetCore.Mvc;
using RESTApi.DataBase.Data;

namespace RESTApi.Controllers;

[Route("api/user")]
[ApiController]
[Produces("application/json")]
public class UsersController : BaseController
{
    public UsersController(F1DataBaseContext context) : base(context)
    {
    }

    [HttpGet("roles")]
    public async Task<IEnumerable<string>> GetRoles()
    {
        return await GetRolesByUser();
    }
}