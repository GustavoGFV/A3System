namespace A3System.Interface
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userRole);
    }
}
