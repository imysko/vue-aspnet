using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTApi.DataBase.Data;
using RESTApi.DataBase.Models;

namespace RESTApi.Controllers
{
    [Route("api/teams")]
    [ApiController]
    [Produces("application/json")]
    public class TeamsController : BaseController
    {
        public TeamsController(F1DataBaseContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams(int? teamId)
        {
            return teamId switch
            {
                null => await _context.Teams
                    .OrderBy(i => i.Title)
                    .ToListAsync(),
                _ => await _context.Teams
                    .Where(i => i.Id == teamId)
                    .OrderBy(i => i.Title)
                    .ToListAsync()
            };
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            return team == null ? NotFound() : team;
        }

        [HttpPost]
        public async Task<JsonResult> PostTeam(Team team)
        {
            if (!CheckSession())
            {
                return new JsonResult(new BaseResponse(false, "Доступ запрещён"));
            }

            var roles = await GetRolesByUser();
            
            if (!(roles.Contains(Roles.Editor.ToString()) || roles.Contains(Roles.Superuser.ToString())))
            {
                return new JsonResult(new BaseResponse(false, "Доступ запрещён"));
            }
            
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return new JsonResult(new BaseResponse(true, "Команда добавлена"));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutTeam(int id, Team team)
        {
            if (!CheckSession())
            {
                return new JsonResult(new BaseResponse(false, "Доступ запрещён"));
            }

            var roles = await GetRolesByUser();
            
            if (!(roles.Contains(Roles.Editor.ToString()) || roles.Contains(Roles.Superuser.ToString())))
            {
                return new JsonResult(new BaseResponse(false, "Доступ запрещён"));
            }
            
            if (id != team.Id)
            {
                return new JsonResult(new BaseResponse(false, "Команда не найдена"));
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return new JsonResult(new BaseResponse(false, "Команда не найдена"));
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new BaseResponse(true, "Команда изменена"));
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteTeam(int id)
        {
            if (!CheckSession())
            {
                return new JsonResult(new BaseResponse(false, "Доступ запрещён"));
            }

            var roles = await GetRolesByUser();
            
            if (!(roles.Contains(Roles.Editor.ToString()) || roles.Contains(Roles.Superuser.ToString())))
            {
                return new JsonResult(new BaseResponse(false, "Доступ запрещён"));
            }
            
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return new JsonResult(new BaseResponse(false, "Команда не найдена"));
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return new JsonResult(new BaseResponse(true, "Команда удалена"));
        }

        private bool TeamExists(int id)
        {
            return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
