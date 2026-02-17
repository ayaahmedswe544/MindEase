namespace MindEase.IService
{
    public interface IJwtService
    {
        string GenerateToken(string id, string email, string role);
    }
}
