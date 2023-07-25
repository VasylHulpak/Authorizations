using System.Web.Http.Controllers; 
using System.Web.Http.Filters;

namespace BasicAuthenticationWebApi.Filters;

public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
{
	private const string Realm = "My Realm";

	public override void OnAuthorization(HttpActionContext actionContext)
	{
		
	}
}
