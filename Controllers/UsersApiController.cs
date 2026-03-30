using AspKnP231.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspKnP231.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UsersApiController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized(new { message = "Помилка 401: Ви не ввійшли в систему." });
            }

            Guid userId = Guid.Parse(userIdString);

            var userAccess = _dataContext.UserAccesses
                .Include(ua => ua.UserRole)
                .FirstOrDefault(ua => ua.UserId == userId);

            if (userAccess == null || userAccess.UserRole.Name != "Admin")
            {
                return StatusCode(403, new { message = "Помилка 403: Доступ заборонено. Ви не адміністратор." });
            }

            var allUsers = _dataContext.UsersData.ToList();

            return Ok(allUsers);
        }






        // Д.З. Доповнити АРІ-контролер перегляду користувачів (попереднє ДЗ) методом,
        //що видаватиме відомості про окремого користувача за його ID або логіном /api/users 
        //-- всі користувачі /api/users/admin -- окремий користувач /api/users/523409870-12934-1235471-51235 -- окремий користувач теперь это

        [HttpGet("{idOrLogin}")]
        public IActionResult GetUser(string idOrLogin)
        {

            AspKnP231.Data.Entities.UserAccess? user = null;

            if (Guid.TryParse(idOrLogin, out Guid parsedId))
            {
                user = _dataContext.UserAccesses
                    .Include(u => u.UserData)
                    .FirstOrDefault(u => u.UserId == parsedId);
            }
            else
            {
                user = _dataContext.UserAccesses
                    .Include(u => u.UserData)
                    .FirstOrDefault(u => u.Login == idOrLogin);
            }
            if (user == null)
            {
                return NotFound(new { message = "Користувача не знайдено." });
            }

            return Ok(new
            {
                id = user.UserId,
                login = user.Login,
                name = user.UserData?.Name,
                email = user.UserData?.Email,
                role = user.UserRoleId
            });
        }
    }

}