using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTApi.DataBase.Data;
using RESTApi.DataBase.Models;

namespace RESTApi.Controllers
{
    [Route("api/cars")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly F1DataBaseContext _context;
        private static IWebHostEnvironment? _webHostEnvironment;

        public CarsController(F1DataBaseContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet, AllowAnonymous]
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
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            return car == null ? NotFound() : car;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost, Authorize(Roles = "user, editor, superuser")]
        public async Task<IActionResult> PostCar([FromForm] CarViewModel viewModel)
        {
            if (_webHostEnvironment == null)
                return StatusCode(415, "Failed to load media file");
            
            var fileName = DateTime.Now.Ticks + ".png";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "media", fileName);

            await using var fileSteam = new FileStream(filePath, FileMode.Create);
            await viewModel.Image.CopyToAsync(fileSteam);

            var newObject = new Car
            {
                Id = 0,
                Title = viewModel.Title,
                Information = viewModel.Information ?? string.Empty,
                ImageUrl = $"/media/{fileName}",
                Description = viewModel.Description,
                TeamId = viewModel.TeamId
            };

            _context.Cars.Add(newObject);
            await _context.SaveChangesAsync();

            return StatusCode(201, "Car was created");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{id}"), Authorize(Roles = "editor, superuser")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
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
                    return NotFound();
                }

                throw;
            }

            return Ok("Car was changed");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{id}"), Authorize(Roles = "editor, superuser")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Ok("Car was deleted");
        }

        private bool CarExists(int id)
        {
            return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
