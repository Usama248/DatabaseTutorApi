using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class Constants
    {
        public static class JWTConfiguration
        {
            public static readonly string JWTIssuer = Utils._config["Jwt:Issuer"];
            public static readonly string JWTAudience = Utils._config["Jwt:Audience"];
            public static readonly string JWTKey = Utils._config["Jwt:Key"];
        }

        public static class SwaggerConfiguration
        {
            public static readonly string SwaggerEndPointURL = Utils._config["SwaggerConfigurations:SwaggerEndPointURL"];
            public static readonly string SwaggerEndPointName = Utils._config["SwaggerConfigurations:SwaggerEndPointName"];
        }
        public static class ConnectionStringHost
        {
            public static readonly string DataSource = Utils._config["ConnectionStringHost:DataSource"];
            public static readonly string Credentials = Utils._config["ConnectionStringHost:Credentials"];
        }
        public static class CustomClaims
        {
            public static readonly string OrganizationId = "OrganizationId";
            public static readonly string UserId = "UserId";
            public static readonly string Role = "Role";
        }

        public static class ResponseStrings
        {
            public static readonly string NotFound = "Not Found";
            public static readonly string Success = "Success";
            public static readonly string Unauthorized = "You are currently blocked. Please try to contact customer support.";
            public static readonly string InvalidCredentials = "Invalid username or password";
            public static readonly string InvalidPassword = "Invalid current password";
        }

        public static class Roles
        {
            public static readonly string Admin = "Admin";
            public static readonly string Student = "Student";
            public static readonly string Teacher = "Teacher";
        }
    }
}
