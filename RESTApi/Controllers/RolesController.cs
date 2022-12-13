using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTApi.DataBase.Data;
using RESTApi.DataBase.Models;

namespace RESTApi.Controllers;

[Route("api/roles")]
[ApiController]
[Produces("application/json")]
public class RolesController : BaseController
{
    public RolesController(F1DataBaseContext context) : base(context)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        return await _context.Roles
            .OrderBy(i => i.Name)
            .ToListAsync();
    }
}