﻿namespace Models.Responses
{
    public class ApiResponse
    {
        public bool Success { get; set; }

        public Object? Content { get; set; }

        public ErrorTypes? Error { get; set; }

        public bool TryRefreshToken { get; set; }
    }
}
