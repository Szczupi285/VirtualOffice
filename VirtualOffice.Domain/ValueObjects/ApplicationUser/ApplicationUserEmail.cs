using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public sealed record ApplicationUserEmail
    {
        public string Email { get; }
        public ApplicationUserEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ApplicationUserEmailCannotBeEmpty();
            try
            {
                new MailAddress(email);
                Email = email;
            }
            catch
            {
                throw new InvalidApplicationUserNameException(email);
            }
        }

        public static implicit operator ApplicationUserEmail(string email)
            => new ApplicationUserEmail(email);

        public static implicit operator string(ApplicationUserEmail email)
            => email.Email;


    }
}
