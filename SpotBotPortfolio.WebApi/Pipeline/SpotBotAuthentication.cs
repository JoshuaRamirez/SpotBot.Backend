using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using SpotBot.Server.Api.Requests;
using SpotBot.Server.Database.Core;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SpotBot.WebApi.Pipeline
{

    public class SpotBotAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public SpotBotAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authenticationHeader = "Authorization";
            if (!Request.Headers.ContainsKey(authenticationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("No token in request headers"));
            }
            var authHeader = Request.Headers[authenticationHeader].ToString();
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token"));
            }
            var tokenString = authHeader.Substring("Bearer ".Length).Trim();
            var token = new Guid(tokenString);
            var isValidToken = checkTokenAgainstDatabase(token);
            if (!isValidToken)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token"));
            }
            var identity = new ClaimsIdentity(Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private bool checkTokenAgainstDatabase(Guid clientToken)
        {
            var getUserTokenRequest = new GetUserTokenRequest();
            getUserTokenRequest.Token = clientToken;
            var savedToken = getUserTokenRequest.Execute();
            if (savedToken == null)
            {
                return false;
            }
            var result = savedToken.Token == clientToken;
            return result;
        }


    }
}