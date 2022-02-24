namespace UsedCar.Backend.Presentations.Functions.Users.Validations;

public interface IDoValidation
{
    bool IsFilledRequired();

    bool IsUtc();
}