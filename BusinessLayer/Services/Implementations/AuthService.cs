using BusinessLayer.DTOs.Auth;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using BusinessLayer.ExternalServices.Abstractions;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLayer.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly ICompanyReadRepository _companyReadRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IMapper _mapper;

    public AuthService(
        IUserReadRepository userReadRepository,
        ICompanyReadRepository companyReadRepository,
        IJwtTokenService jwtTokenService,
        IMapper mapper)
    {
        _userReadRepository = userReadRepository;
        _companyReadRepository = companyReadRepository;
        _jwtTokenService = jwtTokenService;
        _mapper = mapper;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
    {
        var user = await _userReadRepository.GetOneByCondition(
            u => u.UserName == request.UserName,
            false,
            "Company"
        ) ?? throw new UnauthorizedAccessException("Invalid username or password.");
        bool passwordMatches = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!passwordMatches)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        if (user.Company != null && user.Company.PackageEndDate < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Company package has expired. Please contact support to renew.");
        }

        var token = await _jwtTokenService.GenerateJwtToken(user);

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadToken(token) as JwtSecurityToken;
        DateTime expiresAt = jwtSecurityToken?.ValidTo ?? DateTime.UtcNow.AddMinutes(60);

        var response = _mapper.Map<LoginResponseDTO>(user);
        response.Token = token;
        response.ExpiresAt = expiresAt;
        response.Role = user.Role;

        return response;
    }

    public Task<bool> LogoutAsync()
    {
        return Task.FromResult(true);
    }
}