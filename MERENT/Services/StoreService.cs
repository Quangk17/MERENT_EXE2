using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.StoreDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ServiceResponse<StoreDTO>> CreateStoreAsync(StoreCreateDTO store)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<StoreDTO>> DeleteStoreAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<StoreDTO>> GetStoreByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<StoreDTO>>> GetStoresAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<StoreDTO>>> SearchStoreByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<StoreDTO>> UpdateStoreAsync(int id, StoreUpdateDTO updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
