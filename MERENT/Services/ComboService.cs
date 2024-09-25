using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ComboDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ComboService : IComboService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComboService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ServiceResponse<ComboDTO>> AddComboAsync(ComboCreateDTO combo)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ComboDTO>> DeleteComboAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ComboDTO>> GetComboByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<ComboDTO>>> GetCombosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<ComboDTO>>> SearchComboByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ComboDTO>> UpdateComboAsync(int id, ComboUpdateDTOs updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
