﻿using System.Threading.Tasks;
using Net.Architecture.Core.Utilities.Result;

namespace Net.Architecture.Business.Abstract.Common
{
    public interface IAddressService
    {
        Task<IServiceResult<AddressDto>> GetAddress(long id);
        Task<IServiceResult<AddressDto>> InsertAddress(AddressDto addressDto);
        Task<IServiceResult> UpdateAddress(AddressDto addressDto);
    }
}
