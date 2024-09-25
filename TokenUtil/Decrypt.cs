using System.IdentityModel.Tokens.Jwt;

namespace sassy.bulk.TokenUtil
{
    /// <summary>
    /// Decrypt the JWT token.
    /// </summary>
    public static class Decrypt
    {
        /// <summary>
        /// Finds the business name from a JWT token.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        /// <returns>The business name, or an empty string if not found.</returns>
        public static string FindBusinness(string token)
        {
            string businessName = "";
            if (!string.IsNullOrEmpty(token))
            {
                var jwtToken = JWTTokenDecrypt(token); 
                if (jwtToken.Payload.ContainsKey("extension_Business"))
                {
                    businessName = (string)jwtToken.Payload["extension_Business"];
                    businessName = businessName.ToUpper().Trim().Replace(" ", "_");
                    return businessName;
                }
            }
            return businessName;
        }
        /// <summary>
        /// Finds the user ID from a JWT token.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        /// <returns>The user ID, or an empty string if not found.</returns>
        public static string FindUserId(string token)
        {
            string userId = "";
            if (!string.IsNullOrEmpty(token))
            {
                var jwtToken = JWTTokenDecrypt(token);
                if (jwtToken.Payload.ContainsKey("oid"))
                {
                    userId = (string)jwtToken.Payload["oid"];
                    return userId;
                }
            }
            return userId;
        }
        private static JwtSecurityToken JWTTokenDecrypt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            return jsonToken as JwtSecurityToken;
        }
    }
}
