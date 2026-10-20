namespace InventoryManagement.Application.Dtos;

public enum UserState
{
    RegisteredSuccessfully,
    UserExist,
    RegistrationFailed,
    UserNotFound,
    EmailAlreadyConfirmed,
    InvalidToken,
    EmailConfirmedSuccessfully,
    EmailConfirmationFailed,
    EmailNotConfirmed,
    RestPasswordSent,
    EmailConfirmationSent,
    PasswordResetFailed,
    PasswordResetSuccessfully,
}