using AutoMapper;
using CommonLayer;
using CommonLayer.Helper;
using CommonLayer.Helpers.TokenService;
using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDtos.User;
using DatabaseTutor.DTOs.ResponseDTOs.User;
using EntityLayer.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static CommonLayer.Constants;

namespace RepositoryLayer.Repos
{
    public class UserRepo : RepositoryBase<DatabaseTutorUser>, IUserRepo
    {
        private readonly IMapper _mapper;

        public UserRepo(IServiceProvider serviceProvider, DatabaseTutorDbContext context) : base(context, serviceProvider)
        {
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }
        public async Task<ResponseDTO<LoginResponseDTO>> ProcessLogin(RequestDTO<LoginRequestDTO> model)
        {
            var licUser = _serviceProvider.GetRequiredService<IUserRepo>().Get().FirstOrDefault(p => p.Email == model.Data.Email);
            if (licUser == null)
                return Responses.BadRequest<LoginResponseDTO>("Invalid username or password!", null);

            var _userManager = _serviceProvider.GetRequiredService<UserManager<DatabaseTutorUser>>();
            var user = await _userManager.FindByEmailAsync(model.Data.Email);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!user.EmailConfirmed)
                    return Responses.BadRequest<LoginResponseDTO>("User Email not verified", null);
                var signIn = await _serviceProvider.GetRequiredService<SignInManager<DatabaseTutorUser>>().PasswordSignInAsync(user, model.Data.Password, true, true);
                if (signIn.Succeeded)
                {
                    var _tokenService = _serviceProvider.GetRequiredService<ITokenService>();
                    var claims = new List<Claim>()
                    {
                        new Claim("UserId", user.Id),
                        new Claim("UserName",user.UserName),
                        new Claim("Role", userRoles.FirstOrDefault())
                    };
                    var _roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var role = await _roleManager.FindByNameAsync(userRoles.FirstOrDefault());
                    var claimsList = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(claimsList.ToList());
                    foreach (var item in userRoles)
                        claims.Add(new Claim(ClaimTypes.Role, item));

                    var accessToken = _tokenService.GenerateAccessToken(claims);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    var userDetails = _mapper.Map<UserResponseDTO>(user);
                    return Responses.OKGet("User Details", new LoginResponseDTO() { AccessToken = accessToken, RefreshToken = refreshToken, AccountDetails = userDetails });
                }
                else if (signIn.IsLockedOut)
                    return Responses.BadRequest<LoginResponseDTO>("Too many invalid attempts! Account locked out for 5 minutes", null);
                else
                    return Responses.BadRequest<LoginResponseDTO>("Invalid username or password!", null);
            }
            else
                return Responses.BadRequest<LoginResponseDTO>("Invalid username or password!", null);
        }

        public async Task<ResponseDTO<RegisterResponseDTO>> Register(RequestDTO<RegisterRequestDTO> model)
        {
            var uniqueGuid = Guid.NewGuid();
            var _userManager = _serviceProvider.GetRequiredService<UserManager<DatabaseTutorUser>>();
            var userResult = await _userManager.CreateAsync(new DatabaseTutorUser()
            {
                Id = uniqueGuid.ToString(),
                Email = model.Data.Email,
                FirstName = model.Data.FirstName,
                LastName = model.Data.LastName,
                PhoneNumber = model.Data.Contact,
                CreatedDate = DateTime.UtcNow,
                UserName = model.Data.Email,
            }, model.Data.Password);
            if (userResult.Succeeded)
            {
                var roleResult = await AddRoles();
                var addToRoleResult = await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(model.Data.Email), Roles.Teacher);
                    if (addToRoleResult.Succeeded)
                    {
                        var user = await _userManager.FindByEmailAsync(model.Data.Email);
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        return Responses.OKAdded($"{model.Data.FirstName} {model.Data.LastName}", new RegisterResponseDTO() { Name = $"{model.Data.FirstName} {model.Data.LastName}" });
                    }
            }

            return Responses.BadRequest<RegisterResponseDTO>("Error while registering new User", null);
        }

        public async Task<ResponseDTO<UserResponseDTO>> GetUserDetails()
        {
            var _userManager = _serviceProvider.GetRequiredService<UserManager<DatabaseTutorUser>>();
            var user = await _userManager.FindByIdAsync(Utils.GetUserId(_serviceProvider));
            if (user == null)
                return Responses.NotFound<UserResponseDTO>("User", null);

            return Responses.OK("User Details get successfully", new UserResponseDTO()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Name = user.FirstName + " " + user.LastName,
                Role = Utils.GetRole(_serviceProvider)
            });
        }


        private async Task<ResponseDTO<bool>> AddRoles()
        {
            var isSuccessfull = true;
            var _roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new List<IdentityRole>()
            {
               new IdentityRole() { Name = Roles.Admin},
               new IdentityRole() { Name = Roles.Student},
               new IdentityRole() { Name = Roles.Teacher},
            };
            foreach (var role in roles)
            {
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                    isSuccessfull = false;
            }

            if (isSuccessfull)
                return Responses.OKAdded("Roles", isSuccessfull);
            else
                return Responses.BadRequest("Error While Adding Roles", isSuccessfull);
        }


    }
}
