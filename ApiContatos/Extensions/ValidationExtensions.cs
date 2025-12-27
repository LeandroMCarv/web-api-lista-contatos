using System.Net.Mail;

namespace ApiContatos.Extensions;

public static class ValidationExtensions
{
    public static bool EmailValido(string email)
    {
        try
        {
            var mail = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
