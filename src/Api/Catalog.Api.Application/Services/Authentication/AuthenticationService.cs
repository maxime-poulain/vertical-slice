using System.Security.Cryptography;
using Catalog.Api.EfCore.Context;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Services.Authentication;

public interface IPasswordHasher
{
    string HashPassword(string password);
}

public class PasswordHasher : IPasswordHasher
{
    private  const int IterationCount = 100_000;
    private static readonly RandomNumberGenerator _defaultRng = RandomNumberGenerator.Create();

    public virtual string HashPassword(string password)
    {
        return Convert.ToBase64String(HashPasswordV3(password, _defaultRng));
    }

    private byte[] HashPasswordV3(string password, RandomNumberGenerator rng)
    {
        return HashPasswordV3(password, rng,
            prf: KeyDerivationPrf.HMACSHA512,
            iterCount: IterationCount,
            saltSize: 128 / 8,
            numBytesRequested: 256 / 8);
    }

    private static byte[] HashPasswordV3(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
    {
        // Produce a version 3 (see comment above) text hash.
        byte[] salt = new byte[saltSize];
        rng.GetBytes(salt);
        byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

        var outputBytes = new byte[13 + salt.Length + subkey.Length];
        outputBytes[0] = 0x01; // format marker
        WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
        return outputBytes;
    }

    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }
}

public class AuthenticationService : IAuthenticationService
{
    private readonly CatalogContext _catalogContext;
    private readonly IPasswordHasher _passwordHasher;

    public AuthenticationService(CatalogContext catalogContext, IPasswordHasher passwordHasher)
    {
        _catalogContext = catalogContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticateRequest authenticateRequest)
    {
        var trainer = await _catalogContext.Trainer.SingleOrDefaultAsync(trainer => trainer.Email == authenticateRequest.Email);

        if (trainer is null)
        {
            return AuthenticationResponse.InvalidUserOrNotFound;
        }

        var passwordMatch = _passwordHasher.VerifyPassword(authenticateRequest.Password, trainer.Hash, trainer.Salt);
    }

    public virtual string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(password);
    }
}

public interface IAuthenticationService
{
    public Task<AuthenticationResponse> AuthenticateAsync(AuthenticateRequest authenticateRequest);

}
