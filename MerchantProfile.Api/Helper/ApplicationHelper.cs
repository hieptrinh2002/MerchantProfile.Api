using System.Security.Cryptography;

namespace MerchantProfile.Api.Helper;

public class ApplicationHelper
{
    public static string GenerateToken()
    {
        var random = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(random);

            return Convert.ToBase64String(random);
        }
    }
}
