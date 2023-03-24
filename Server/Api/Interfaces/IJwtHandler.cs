using CoreLayer.Entities;
using Microsoft.Extensions.Primitives;

namespace Api.Interfaces {
    public interface IJwtHandler {
        public string GenerateJwtToken(Account account);
        public string GetAccountIdFromJwt(StringValues headerValue); 
    }
}