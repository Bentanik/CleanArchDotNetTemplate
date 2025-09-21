namespace _Project_.Contracts.Messages;

public enum AppMessages
{
    [Message("Not found", "APP001")]
    NotFound,

    [Message("Already exists", "APP002")]
    AlreadyExists,

    [Message("Invalid input", "APP003")]
    InvalidInput
}