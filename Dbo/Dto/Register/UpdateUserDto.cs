﻿namespace A3System.Dbo.Dto.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
        public string? RePassword { get; set; }
    }
}