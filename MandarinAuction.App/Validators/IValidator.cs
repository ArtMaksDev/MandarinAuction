namespace MandarinAuction.App.Validators;

public interface IValidator<T>
{
    void Validate(T value);
}