using Draft.Core.Infrastructure.Abstractions;
using Draft.Infrastructures.Models.Entities;
using Draft.Infrastructures.Models.Jwt;
using Draft.Infrastructures.Models.Jwt.Enums;
using Newtonsoft.Json;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class JwtProvider : IJwtProvider
{
    /// <summary>
    /// IHashProvider
    /// </summary>
    private readonly IHashProvider _hashProvider;

    /// <summary>
    /// IConfigProvider
    /// </summary>
    private readonly IConfigProvider _configProvider;

    /// <summary>
    /// ILogProvider
    /// </summary>
    private readonly ILogProvider _logProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hashProvider"></param>
    /// <param name="configProvider"></param>
    /// <param name="logProvider"></param>
    public JwtProvider(IHashProvider hashProvider, IConfigProvider configProvider, ILogProvider logProvider)
    {
        _hashProvider = hashProvider;
        _configProvider = configProvider;
        _logProvider = logProvider;
    }

    ///<inheritdoc/>
    public string GenerateJwt(Account account, string audiencer, string issuer, string sessionId, JwtType type)
    {
        try
        {
            var header = new JwtHeader
            {
                Type = "JWT",
                Alogrithm = "HS256"
            };

            var payload = new JwtPayload
            {
                AccountId = account.Id,
                Email = account.Email,
                Fullname = account.Fullname,
                Avatar = account.Avatar ??= string.Empty,
                Audiencer = audiencer,
                Issuer = issuer,
                Role = account.Type
            };

            var secret = "";
            var expiredTime = DateTime.Now;

            if (type == JwtType.AccessToken)
            {
                secret = _configProvider.GetConfigString("AccessTokenSecret");
                var expiredMinutes = _configProvider.GetConfigString("AccessTokenExpiredTime");
                expiredTime = expiredTime.AddMinutes(int.Parse(expiredMinutes));
            }
            else
            {
                secret = _configProvider.GetConfigString("RefreshTokenSecret");
                var expiredDays = _configProvider.GetConfigString("RefreshTokenExpiredTime");
                expiredTime = expiredTime.AddDays(int.Parse(expiredDays));
            }

            payload.Expired = expiredTime;
            var headerJson = JsonConvert.SerializeObject(header);
            var headerBase64 = _hashProvider.Base64Encode(headerJson);
            var payloadJson = JsonConvert.SerializeObject(payload);
            var payloadBase64 = _hashProvider.Base64Encode(payloadJson);
            var signatureBefore = headerBase64 + "." + payloadBase64;
            var signatureAfter = _hashProvider.SHA256Encrypt(signatureBefore, secret);
            return signatureBefore + "." + signatureAfter;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return "";
        }
    }

    ///<inheritdoc/>
    public bool ValidateJwt(string jwt, JwtType type)
    {
        try
        {
            var jwtArr = jwt.Split('.');

            if (jwtArr.Length == 3)
            {
                var headerBase64 = jwtArr[0];
                var payloadBase64 = jwtArr[1];
                var signatureBase64 = jwtArr[2];
                var secret = "";

                if (type == JwtType.AccessToken)
                {
                    secret = _configProvider.GetConfigString("AccessTokenSecret");
                }
                else
                {
                    secret = _configProvider.GetConfigString("RefreshTokenSecret");
                }

                var signatureBefore = headerBase64 + "." + payloadBase64;
                var signatureAfter = _hashProvider.SHA256Encrypt(signatureBefore, secret);

                if (signatureAfter.Equals(signatureBase64))
                {
                    var payloadJson = _hashProvider.Base64Decode(payloadBase64);
                    var payload = JsonConvert.DeserializeObject<JwtPayload>(payloadJson);

                    if (payload != null)
                    {
                        if (payload.Expired <= DateTime.Now.AddSeconds(-10))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }

    ///<inheritdoc/>
    public (JwtHeader? header, JwtPayload? payload) DecodeJwt(string token, JwtType type)
    {
        if (ValidateJwt(token, type))
        {
            var jwtArr = token.Split('.');

            if (jwtArr.Length == 3)
            {
                var headerBase64 = jwtArr[0];
                var headerJson = _hashProvider.Base64Decode(headerBase64);
                var payloadBase64 = jwtArr[1];
                var payloadJson = _hashProvider.Base64Decode(payloadBase64);
                var header = JsonConvert.DeserializeObject<JwtHeader>(headerJson);
                var payload = JsonConvert.DeserializeObject<JwtPayload>(payloadJson);
                return (header, payload);
            }
        }

        return (null, null);
    }

    ///<inheritdoc/>
    public JwtPayload? DecodeJwtToPayload(string token, JwtType type)
    {
        if (ValidateJwt(token, type))
        {
            var jwtArr = token.Split('.');

            if (jwtArr.Length == 3)
            {
                var payloadBase64 = jwtArr[1];
                var payloadJson = _hashProvider.Base64Decode(payloadBase64);
                var payload = JsonConvert.DeserializeObject<JwtPayload>(payloadJson);
                return payload;
            }
        }

        return null;
    }
}
