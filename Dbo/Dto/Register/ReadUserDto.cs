﻿namespace A3System.Dbo.Dto.User

{
    public class ReadUserDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }
    }
}

