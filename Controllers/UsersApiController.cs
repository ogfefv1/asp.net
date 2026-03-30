using AspKnP231.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspKnP231.Controllers
{

    [Route("api/[controller]")]
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
    }
}