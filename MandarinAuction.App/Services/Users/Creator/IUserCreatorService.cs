namespace MandarinAuction.App.Services.Users.Creator;

public interface IUserCreatorService<in T>
{
    public Task<Guid> Create(T data);
}