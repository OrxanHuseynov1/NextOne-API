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
        ) ?? throw new UnauthorizedAccessException("İstifadəçi adı və ya şifrə yanlışdır.");

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!passwordMatches)
        {
            throw new UnauthorizedAccessException("İstifadəçi adı və ya şifrə yanlışdır.");
        }

        if (user.Company == null)
        {
            throw new UnauthorizedAccessException("İstifadəçinin əlaqəli olduğu şirkət tapılmadı.");
        }

        if (user.Company.PackageEndDate < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Şirkətin paketi bitib. Yeniləmək üçün əlaqə saxlayın.");
        }

        var token = await _jwtTokenService.GenerateJwtToken(user);

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadToken(token) as JwtSecurityToken;
        DateTime expiresAt = jwtSecurityToken?.ValidTo ?? DateTime.UtcNow.AddMinutes(60);

        var response = _mapper.Map<LoginResponseDTO>(user);
        response.Token = token;
        response.ExpiresAt = expiresAt;
        response.Role = user.Role; 

        response.CompanyName = user.Company.Name;
        response.PackageEndDate = user.Company.PackageEndDate;

        return response;
    }

    public Task<bool> LogoutAsync()
    {
        return Task.FromResult(true);
    }
}