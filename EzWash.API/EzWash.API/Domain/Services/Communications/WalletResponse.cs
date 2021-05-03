using EzWash.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Services.Communications
{
    public class WalletResponse : BaseResponse<Wallet>
    {
        

        public WalletResponse(string message) : base(message)
        {
        }

        public WalletResponse(Wallet resource) : base(resource)
        {
        }
    }
}
