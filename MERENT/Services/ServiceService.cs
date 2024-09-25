using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ServiceDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ServiceResponse<ServiceDTO>> CreateServiceAsync(ServiceCreateDTO store)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ServiceDTO>> DeleteServiceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ServiceDTO>> GetServiceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<ServiceDTO>>> GetServicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<ServiceDTO>>> SearchServiceByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ServiceDTO>> UpdateServiceAsync(int id, ServiceUpdateDTO updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
