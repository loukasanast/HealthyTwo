using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    class AuthData
    {
        public string Token_type { get; set; }
        public int Expires_in { get; set; }
        public string Scope { get; set; }
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
        public string User_id { get; set; }
    }
}
