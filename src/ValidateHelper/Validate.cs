using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ValidateHelper
{
    public static class Validate
    {
        public static void NotNull(object value, string parameterName,string field = null)
        {
            if (value == null) throw new ArgumentNullException(parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void MustBeNull(object value, string parameterName, string field = null)
        {
            if (value != null) throw new ArgumentException($"Parameter must be null", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void NotNullOrEmptyOrWhiteSpace(string value, string parameterName, string field = null)
        {
            if (value == null) throw new ArgumentNullException(parameterName + (field == null ? string.Empty : "." + field));
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"Parameter can't be an empty or whitespace string", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void NotNullOrEmptyArray<T>(T[] values, string parameterName, string field = null)
        {
            if (values == null) throw new ArgumentNullException(parameterName + (field == null ? string.Empty : "." + field));
            if (values.Length == 0) throw new ArgumentException($"Parameter can't be an empty array", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void NotNullOrContainingNullArray<T>(IEnumerable<T> values,  string parameterName,  string field = null)
        {
            if (values == null) throw new ArgumentNullException(parameterName + (field == null ? string.Empty : "." + field));
            if (values.Any(_ => _ == null)) throw new ArgumentException($"Parameter cannot contain Null values", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void NotContainingNullArray<T>( IEnumerable<T> values,  string parameterName,  string field = null)
        {
            if (values.Any(_ => _ == null)) throw new ArgumentException($"Parameter cannot contain Null values", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void NotContainingNullOrWhiteSpaceStringArray( IEnumerable<string> values,  string parameterName,  string field = null)
        {
            if (values.Any(_ => string.IsNullOrWhiteSpace(_))) throw new ArgumentException($"Parameter cannot contain null or empty strings", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void NotEmpty(Guid id,  string parameterName,  string field = null)
        {
            if (id == Guid.Empty) throw new ArgumentException("Parameter can't be empty", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void Any(bool[] arrayOfValidations,  string parameterName,  string field = null)
        {
            if (!arrayOfValidations.Any(_ => _)) throw new ArgumentException("Parameter is not valid", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void All(bool[] arrayOfValidations,  string parameterName,
             string field = null)
        {
            if (!arrayOfValidations.All(_ => _))
                throw new ArgumentException("Parameter is not valid",
                    parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void Positive(int value,  string parameterName,  string field = null)
        {
            if (value <= 0) throw new ArgumentException($"Parameter must be greater than 0", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void PositiveOrZero(int value,  string parameterName,  string field = null)
        {
            if (value < 0) throw new ArgumentException($"Parameter must be greater than or equal to 0", parameterName + (field == null ? string.Empty : "." + field));
        }

        public static void Between(double value, double min, double max,  string parameterName,  string field = null)
        {
            if (value < min || value > max) throw new ArgumentException($"Parameter must be greater than or equal to {min} and less than or equal to {max}", parameterName + (field == null ? string.Empty : "." + field));
        }

        private static readonly Regex ValidUsernameRegex = new Regex(@"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$", RegexOptions.Compiled);

        public const int MinUsernameLenght = 5;
        public static void Username( string username,  string parameterName,  string field = null)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException($"Username can't be an empty or whitespace string", parameterName + (field == null ? string.Empty : "." + field));
            if (username.Length < MinUsernameLenght) throw new ArgumentException($"Username must be at least {MinUsernameLenght} characters", parameterName + (field == null ? string.Empty : "." + field));
            if (!ValidUsernameRegex.IsMatch(username) && !ValidEmailRegex.IsMatch(username)) throw new ArgumentException("Username contains invalid characters", parameterName + (field == null ? string.Empty : "." + field));
        }

        private static readonly Regex ValidPasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{10,}$", RegexOptions.Compiled);
        public static void Password( string password,  string parameterName,  string field = null, int minPasswordLength = 10)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException($"Password can't be an empty or whitespace string", parameterName + (field == null ? string.Empty : "." + field));
            if (password.IndexOf(' ') > -1) throw new ArgumentException("Password must not contains spaces", parameterName + (field == null ? string.Empty : "." + field));
            if (password.Length < minPasswordLength) throw new ArgumentException($"Password must be at least {minPasswordLength} characters", parameterName + (field == null ? string.Empty : "." + field));
            if (!ValidPasswordRegex.IsMatch(password))
                throw new ArgumentException($"Password must contains at least one uppercase letter, one lowercase letter, one number and one special character", parameterName + (field == null ? string.Empty : "." + field));
        }

        private static readonly Regex ValidEmailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static void Email( string email,  string parameterName,  string field = null)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException($"Email can't be an empty or whitespace string", parameterName + (field == null ? string.Empty : "." + field));
            if (email.IndexOf(' ') > -1) throw new ArgumentException("Email must not contains spaces", parameterName + (field == null ? string.Empty : "." + field));
            if (!ValidEmailRegex.IsMatch(email)) throw new ArgumentException("Email contains invalid characters", parameterName + (field == null ? string.Empty : "." + field));
        }
    }

}
