namespace Models
{
    public class User : ModelBase
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Token { get; set; }

        //public ErrorType? Error { get; set; }

        //public DateTime LastUpdate { get; set; }
    }

    //public enum ErrorType
    //{
    //    WrongEmailOrPassword, UnknownEmailAddress, EmailExists, UnavailableServer
    //}
}
