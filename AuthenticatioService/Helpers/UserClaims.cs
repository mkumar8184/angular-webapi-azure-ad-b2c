
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AuthenticatioService.Helpers
{
    public class UserClaims
    {       
        public static string GetClaimByForContext(HttpContext httpContext, string claimname)
        {
            string claimValue = string.Empty;
            try
            {
                claimValue = httpContext.User?.Claims.Where(a => a.Type.ToLower().EndsWith(claimname.Trim().ToLower()))?.FirstOrDefault()?.Value;
            }
            catch (Exception ex)
            {
               
            }
            return claimValue??"";
        }
        public static List<string> GetUserRoleByClaims(IEnumerable<Claim> claims)
        {
            List<string> claimValue = new List<string>();
            try
            {
                claimValue = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            }
            catch (Exception ex)
            {
                // Log.Error("UserHelper:GetClaimValue :: Error message => {error}", ex.Message);

            }

            return claimValue;

        }

    }
}
