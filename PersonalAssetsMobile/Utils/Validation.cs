﻿using System.Text.RegularExpressions;

namespace PersonalAssetsMobile.Utils
{
    public static partial class Validations
    {
        public static bool ValidateEmail(string email)
        {
            return EmailValidation().IsMatch(email);
        }

        [GeneratedRegex("\\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z", RegexOptions.IgnoreCase, "pt-BR")]
        private static partial Regex EmailValidation();
    }
}
