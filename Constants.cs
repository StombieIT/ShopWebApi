using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebApi
{
    public static class Constants
    {
        public const string ISSUER = "https://localhost:44391/";
        public const string AUDIENCE = ISSUER;
        private const string SECRET = "bebrANETHUIbufhd";
        public static byte[] KEY => Encoding.UTF8.GetBytes(SECRET);
    }
}
