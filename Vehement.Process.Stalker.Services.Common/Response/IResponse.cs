using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehement.Process.Stalker.Services.Common.Response
{
    public interface IResponse
    {
        string Status { get; set; }

        string Message { get; set; }
    }
}
