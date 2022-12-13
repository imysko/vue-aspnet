using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTApi.DataBase.Data;
using RESTApi.DataBase.Models;

namespace RESTApi.Controllers
{
    [Route("api/cars")]
    [ApiController]
    [Produces("application/json")]
    public class CarsController : BaseController
    {
        private static IWebHostEnvironment? _webHostEnvironment;
        
        public CarsController(F1DataBaseContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars(int? teamId)
        {
            return teamId switch
            {
                null => await _context.Cars
                    .OrderBy(i => i.Title)
                    .ToListAsync(),
                _ => await _context.Cars
                    .Where(i => i.TeamId == teamId)
                    .OrderBy(i => i.Title)
                    .ToListAsync()
            };
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            return car == null ? NotFound() : car;
        }

        [HttpPost]
        public async Task<JsonResult> PostCar([FromForm] CarViewModel viewModel)
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
            
            if (_webHostEnvironment == null) 
                return new JsonResult(new BaseResponse(false, "Не удалось загрузить изображение"));
            
            var fileName = DateTime.Now.Ticks + ".png";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Media", fileName);
            
            await using var fileSteam = new FileStream(filePath, FileMode.Create);
            await viewModel.Image.CopyToAsync(fileSteam);
            
            var newObject = new Car
            {
                Id = 0,
                Title = viewModel.Title,
                Information = viewModel.Information ?? string.Empty,
                ImageUrl = $"{Request.Scheme}://{Request.Host}/media/{fileName}",
                Description = viewModel.Description,
                TeamId = viewModel.TeamId
            };

            _context.Cars.Add(newObject);
            await _context.SaveChangesAsync();

            return new JsonResult(new BaseResponse(true, "Болид добавлен"));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutCar(int id, Car car)
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
            
            if (id != car.Id)
            {
                return new JsonResult(new BaseResponse(false, "Болид не найден"));
            }
            

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return new JsonResult(new BaseResponse(false, "Болид не найден"));
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new BaseResponse(true, "Болид изменён"));
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteCar(int id)
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
            
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return new JsonResult(new BaseResponse(false, "Болид не найден"));
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return new JsonResult(new BaseResponse(true, "Болид удалён"));
        }

        private bool CarExists(int id)
        {
            return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
