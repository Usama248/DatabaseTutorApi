using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static CommonLayer.Constants;

namespace CommonLayer
{
    public static class ExtensionMethods
    {
        public static X509Certificate2 GetCertficateFromStore(this X509Store store, string thumbPrint)
        {
            store.Open(OpenFlags.ReadOnly);
            var cert = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, false);
            if (cert.Count > 0)
                return cert[0];
            else
                return null;
        }


        public static string GetRole(this ClaimsPrincipal principal)
        {
            var data = principal.Claims.ToList().FirstOrDefault(x => x.Type.Trim() == CustomClaims.Role);
            if (data != null)
                return data.Value;
            return "";
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var data = principal.Claims.ToList().FirstOrDefault(x => x.Type.Trim() == CustomClaims.UserId);
            if (data != null)
                return data.Value;
            return "";
        }
    }
}
