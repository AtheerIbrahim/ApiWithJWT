﻿namespace ApiWithJWT.Dtos;

public class AuthenticationResponse
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
