namespace BuildingBlocksClient.Starter.DTOs
{
    public class ServiceResponses
    {
        public record class GeneralResponse(bool Flag, string Message = null!);
        public record class LoginResponse(bool Flag, string Message = null!, string Token = null!);
    }
}
