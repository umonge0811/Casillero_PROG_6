using Microsoft.AspNetCore.Mvc;

namespace Casillero_PROG_6.Filters
{
    public class AdminAuthorizeAttribute : TypeFilterAttribute
    {
        public AdminAuthorizeAttribute() : base(typeof(AdminAuthorizationFilter))
        {
        }
    }
}