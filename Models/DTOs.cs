namespace backend.Models
{
    public record PostUserDTO(string name);
    public record PostContent(Guid userId, string content);
}
