﻿namespace WebApplication5.Models;

public class AppUser
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpirationDate { get; set; }
}