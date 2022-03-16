using _468_.Net_Fundamentals.Domain.Base;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Domain.ViewModels.Authenticate;
using _468_.Net_Fundamentals.Infrastructure;
using _468_.Net_Fundamentals.Service.Authenticator;
using _468_.Net_Fundamentals.Service.TokenGenerators;
using _468_.Net_Fundamentals.Service.TokenValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class AccountService : RepositoryBase<AppUser>, IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly AuthenticatorProvider _authenticator;
        private readonly GetPrincipal _getPrincipal;
        private readonly IUnitOfWork _unitOfWork;


        public AccountService(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, RefreshTokenValidator refreshTokenValidator, AuthenticatorProvider authenticator, IUnitOfWork unitOfWork, GetPrincipal getPrincipal) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _refreshTokenValidator = refreshTokenValidator;
            _authenticator = authenticator;
            _unitOfWork = unitOfWork;
            _getPrincipal = getPrincipal;
        }

        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            var user = await _userManager.FindByEmailAsync(userLoginVM.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginVM.Password))
            {
                /* var userRoles = await _userManager.GetRolesAsync(user);
                 var authClaims = new List<Claim>
                 {
                     new Claim("Id", user.Id),
                     new Claim("Name", user.UserName),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 };
                 foreach (var userRole in userRoles)
                 {
                     authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                 }
                 var authSiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                 var accessToken = new JwtSecurityToken(
                         issuer: _configuration["JWT:ValidIssuer"], 
                         audience: _configuration["JWT:ValidAudience"],
                         expires: DateTime.Now.AddHours(5),
                         claims: authClaims,
                         signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256));*/
                AuthenticatedRespone respone = await _authenticator.Authenticate(user);

                return new OkObjectResult(respone);

            }
            return new UnauthorizedResult();
        }

        public async Task<IActionResult> Refresh(string refreshTokenRequest)
        {

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshTokenRequest);

            if (!isValidRefreshToken)
            {
                return new BadRequestResult();
            }

            RefreshToken refreshTokenDTO = await _unitOfWork.Repository<RefreshToken>()
                .Query()
                .Where(_=>_.Token== refreshTokenRequest)
                .FirstOrDefaultAsync();

            if (refreshTokenDTO == null)
            {
                return new NotFoundResult();
            }

            await _unitOfWork.Repository<RefreshToken>().DeleteAsync(refreshTokenDTO);


            AppUser user = await _userManager.FindByIdAsync(refreshTokenDTO.UserId);

            if (user == null)
            {
                return new NotFoundResult();
            }

            AuthenticatedRespone respone = await _authenticator.Authenticate(user);

            await _unitOfWork.SaveChangesAsync();

            return new OkObjectResult(respone);
        }

        public async Task<IActionResult> Register(UserRegistrationVM userRegistration)
        {
            var userExist = await _userManager.FindByEmailAsync(userRegistration.Email);

            if (userExist != null)
            {
                var resultStatus = new ObjectResult(new Response
                {
                    Status = "Error",
                    Message = "User Already Exist"
                });
                resultStatus.StatusCode = StatusCodes.Status500InternalServerError;
                return resultStatus;
            }

            var user = new AppUser
            {
                UserName = userRegistration.UserName,
                Email = userRegistration.Email,
                FullName = userRegistration.FullName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if (!result.Succeeded)
            {
                var resultStatus = new ObjectResult(new Response
                {
                    Status = "Error",
                    Message = "User Already Exist"
                });
                resultStatus.StatusCode = StatusCodes.Status500InternalServerError;
                return resultStatus;
            }


            if (!await _roleManager.RoleExistsAsync(AppUserRole.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(AppUserRole.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(AppUserRole.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(AppUserRole.User));
            }

            if (await _roleManager.RoleExistsAsync(AppUserRole.User))
            {
                await _userManager.AddToRoleAsync(user, AppUserRole.User);
            }

            return new OkObjectResult(new Response
            {
                Status = "Success",
                Message = "User Created Success"
            });
        }

     


    }
}
