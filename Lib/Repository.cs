using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Repository
    {
        public static async Task<string> Test()
        {
            return await Util.GetRefreshToken();
        }
    }
}
