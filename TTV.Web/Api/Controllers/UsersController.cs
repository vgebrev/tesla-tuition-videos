using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTV.Domain.Entities;
using TTV.Infrastructure.DataAccess;

namespace TTV.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext dataContext;

        public UsersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await dataContext.Users.ToListAsync(cancellationToken);
        }
    }
}
