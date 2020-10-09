using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.Model.Dtos.Account;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Api.AuthHelper
{
    public class JwtHelper
    {
        private readonly JwtSettings _jwtSettings;
        public JwtHelper(IOptions<JwtSettings> _jwtSettingsAccesser)
        {
            this._jwtSettings = _jwtSettingsAccesser.Value;
        }

        public string IssueJwt(TokenModelJwt tokenModelJwt)
        {
            var exp = DateTime.Now.AddSeconds(30 * 1);
            var claims = new List<Claim>{
                //jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
                new Claim(JwtRegisteredClaimNames.Jti,tokenModelJwt.UserId.ToString()),
                //令牌颁发时间
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),//ToUnixTimeSeconds  获取时间戳
                //定义在什么时间之前，该jwt都是不可用的.
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(exp).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,_jwtSettings.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,_jwtSettings.Audience),

                new Claim(ClaimTypes.Sid,tokenModelJwt.UserId.ToString()),
                new Claim(ClaimTypes.Expiration,$"{new DateTimeOffset(exp).ToUnixTimeSeconds()}")
            };
            if (tokenModelJwt.Roles != null)
            {
                claims.AddRange(tokenModelJwt.Roles.Select(r => new Claim(ClaimTypes.Role, r)));
            }
            //对称秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            //签名证书(秘钥，加密算法)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public JwtTokenDto BuildJwtToken(TokenModelJwt tokenModelJwt)
        {
            var exp = DateTime.Now.AddSeconds(30);
            var claims = new List<Claim>{
                //jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
                new Claim(JwtRegisteredClaimNames.Jti,tokenModelJwt.UserId.ToString()),
                //令牌颁发时间
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),//ToUnixTimeSeconds  获取时间戳
                //定义在什么时间之前，该jwt都是不可用的.
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(exp).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,_jwtSettings.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,_jwtSettings.Audience),

                new Claim(ClaimTypes.Sid,tokenModelJwt.UserId.ToString()),
                new Claim(ClaimTypes.Expiration,$"{new DateTimeOffset(exp).ToUnixTimeSeconds()}")
            };
            if (tokenModelJwt.Roles != null)
            {
                claims.AddRange(tokenModelJwt.Roles.Select(r => new Claim(ClaimTypes.Role, r)));
            }
            //对称秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            //签名证书(秘钥，加密算法)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new JwtTokenDto
            {
                Token = token,
                Expiration = exp
            };
        }
        public TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object roles);
            var tm = new TokenModelJwt
            {
                UserId = (jwtToken.Id).ObjToInt()
            };
            try
            {
                tm.Roles = roles != null ? JsonConvert.DeserializeObject<List<string>>(roles.ObjToString()) : new List<string>();
            }
            catch
            {

                tm.Roles = new List<string> { roles.ObjToString() };
            }
            return tm;
        }
    }
}
