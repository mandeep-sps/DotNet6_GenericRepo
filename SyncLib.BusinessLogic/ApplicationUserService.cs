using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SyncLib.BusinessLogic.Interface;
using SyncLib.Model.Common;
using SyncLib.Model.Dto;
using SyncLib.Repository.Database;
using SyncLib.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyncLib.BusinessLogic
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationSettingsModel _appSettings;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public ApplicationUserService(IRepository repository, IMapper mapper, IOptions<ApplicationSettingsModel> options)
        {
            _repository = repository;
            _mapper = mapper;
            _appSettings = options.Value;
        }
        public async Task<ServiceResult<LoginResponseModel>> GetLogin(LoginRequestModel request)
        {
            try
            {
                var user = await _repository.GetAsync<ApplicationUser>(x => x.Username == request.Username && x.UserPassword == request.Password);
                if (user is null)
                    return new ServiceResult<LoginResponseModel>(data: null, "Invalid credentials");


                var resposne = _mapper.Map<LoginResponseModel>(user);
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Jwt.SecretKey);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Username)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };
                resposne.Token = handler.WriteToken(handler.CreateToken(descriptor));
                return new ServiceResult<LoginResponseModel>(resposne, "Token Generated");
            }
            catch (Exception ex)
            {
                return new ServiceResult<LoginResponseModel>(ex, ex.Message);
            }
        }
    }
}
