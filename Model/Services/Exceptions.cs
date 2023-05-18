using System;

namespace Model.Services;

class IncorrectLoginException : Exception
{
    public override string Message => "Incorrect login";
}

class LoginAlreadyExistException : Exception
{
    public override string Message => "Login already exist!";
}

class IncorrectPasswordException : Exception
{
    public override string Message => "Incorrect password";
}
