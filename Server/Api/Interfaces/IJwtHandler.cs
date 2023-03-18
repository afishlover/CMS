using CoreLayer.Entities;

namespace Api.Interfaces {
    public interface IJwtHandler {
        public string GenerateJwtToken(Account account);
    }
}