using System.Security.Claims;

using ShoppingApp.Business.Abstract;

namespace ShoppingApp.Web;

public class ClaimService : IClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpContext _httpContext;

    public int UserId => GetCurrentUserId();

    public ClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpContext = _httpContextAccessor.HttpContext;
    }

    private int GetCurrentUserId()
    {
        var nameIdentifier = _httpContext.User.FindFirst(s => s.Type == ClaimTypes.NameIdentifier);

        if (nameIdentifier is null)
            throw new Exception("User is not logged in.");

        return int.Parse(nameIdentifier.Value);
    }

    public string GetClaimValue(string key)
    {
        return _httpContext.User.FindFirstValue(key);
    }
}
