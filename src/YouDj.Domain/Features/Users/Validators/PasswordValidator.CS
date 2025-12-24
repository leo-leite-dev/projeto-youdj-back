using System.Text.RegularExpressions;
using YouDj.Domain.Features.Common.Exceptions;

namespace YouDj.Domain.Features.Users.Validators;

public static class PasswordValidator
{
    public static void EnsureStrength(string? password)
    {
        if (password is null) throw new DjException("Senha inválida.");
        if (password.Length < 8)
            throw new DjException("A senha deve conter no mínimo 8 caracteres.");

        if (!Regex.IsMatch(password, @"[A-Z]"))
            throw new DjException("A senha deve conter pelo menos uma letra maiúscula.");

        if (!Regex.IsMatch(password, @"[a-z]"))
            throw new DjException("A senha deve conter pelo menos uma letra minúscula.");

        if (!Regex.IsMatch(password, @"\d"))
            throw new DjException("A senha deve conter pelo menos um número.");

        if (!Regex.IsMatch(password, @"[\W_]"))
            throw new DjException("A senha deve conter pelo menos um caractere especial (ex: ! @ # $ %).");

        if (password.Contains(" "))
            throw new DjException("A senha não pode conter espaços.");
    }
}