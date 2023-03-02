//using Microsoft.AspNetCore.Http;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace AccessControl.Services
//{
//    internal class ValidarTokenHandler : DelegatingHandler
//    {
//        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
//                                                               CancellationToken cancellationToken)
//        {
//            HttpStatusCode statusCode;
//            string token;

//            if (!TryRetrieveToken(request, out token))
//            {
//                statusCode = HttpStatusCode.Unauthorized;
//                return base.SendAsync(request, cancellationToken);
//            }

//            try
//            {
//                var claveSecreta = ConfigurationManager.AppSettings["secretKey"];
//                var issuerToken = ConfigurationManager.AppSettings["Issuer"];
//                var audienceToken = ConfigurationManager.AppSettings["Audience"];

//                var securityKey = new SymmetricSecurityKey(
//                    System.Text.Encoding.Default.GetBytes(claveSecreta));

//                SecurityToken securityToken;
//                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
//                TokenValidationParameters validationParameters = new TokenValidationParameters()
//                {
//                    ValidAudience = audienceToken,
//                    ValidIssuer = issuerToken,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,
//                    // DELEGADO PERSONALIZADO PERA COMPROBAR
//                    // LA CADUCIDAD EL TOKEN.
//                    LifetimeValidator = this.LifetimeValidator,
//                    IssuerSigningKey = securityKey
//                };

//                // COMPRUEBA LA VALIDEZ DEL TOKEN
//                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token,
//                                                                     validationParameters,
//                                                                     out securityToken);

//                return base.SendAsync(request, cancellationToken);
//            }
//            catch (SecurityTokenValidationException ex)
//            {
//                statusCode = HttpStatusCode.Unauthorized;
//            }
//            catch (Exception ex)
//            {
//                statusCode = HttpStatusCode.InternalServerError;
//            }

//            return Task<HttpResponseMessage>.Factory.StartNew(() =>
//                        new HttpResponseMessage(statusCode) { });
//        }

//        // RECUPERA EL TOKEN DE LA PETICIÓN
//        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
//        {
//            token = null;
//            IEnumerable<string> authzHeaders;
//            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) ||
//                                              authzHeaders.Count() > 1)
//            {
//                return false;
//            }
//            var bearerToken = authzHeaders.ElementAt(0);
//            token = bearerToken.StartsWith("Bearer ") ?
//                    bearerToken.Substring(7) : bearerToken;
//            return true;
//        }

//        // COMPRUEBA LA CADUCIDAD DEL TOKEN
//        public bool LifetimeValidator(DateTime? notBefore,
//                                      DateTime? expires,
//                                      SecurityToken securityToken,
//                                      TokenValidationParameters validationParameters)
//        {
//            var valid = false;

//            if ((expires.HasValue && DateTime.UtcNow < expires)
//                && (notBefore.HasValue && DateTime.UtcNow > notBefore))
//            { valid = true; }

//            return valid;
//        }
//    }
//}
