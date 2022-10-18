using AuthenticatioService.Extensions;
using AuthenticatioService.Helpers;
using System.Security.Claims;

namespace AuthenticatioService.Services
{
    public class UserData
    {
       
            public int CustomerId { get; set; }
            public string EmployeeId { get; set; }
            public string Name { get; set; }
            public string UserADId { get; set; }
            public string EmailId { get; set; }
            public List<string> Roles { get; set; }
            public string CompanyCode { get; set; }
        public static UserData? GetSessionUser()
        {
            var httpContext = HttpContextHandler.Current;
            if (httpContext.User == null)
            {
                return null;
            }

            return new UserData
            {
                EmployeeId = UserClaims.GetClaimByForContext(httpContext, "extension_employeeId"),
                Name = httpContext.User.FindFirstValue("name"),
                EmailId = UserClaims.GetClaimByForContext(httpContext, "emails"),
                Roles = GetUserClaimsByPrincipal(AddRoleClaimFromAzureClaim()),
                CompanyCode = UserClaims.GetClaimByForContext(httpContext, "extension_companyCode"),
                CustomerId = int.Parse(UserClaims.GetClaimByForContext(httpContext, "extension_customerId")),
                UserADId = UserClaims.GetClaimByForContext(httpContext, "nameidentifier")
            };
        }
        private static IEnumerable<Claim> AddRoleClaimFromAzureClaim()
        {
            var roles = UserClaims.GetClaimByForContext(HttpContextHandler.Current, "extension_userRoles").Split(",").ToList();
            var userClaims = HttpContextHandler.Current.User.Claims.ToList();
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return userClaims;
        }
        private static List<string> GetUserClaimsByPrincipal(IEnumerable<Claim> claims)
        {
            return UserClaims.GetUserRoleByClaims(claims);
        }
       
    }
}
