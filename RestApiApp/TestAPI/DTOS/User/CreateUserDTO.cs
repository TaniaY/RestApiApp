namespace TestAPI.DTOS.User
{
    public record CreateUserDTO(
    string Email,
    string Password,
    string UserName
);

    public record UserExistenceCheckDTO(
  string UserName,
  string Password
);
}
