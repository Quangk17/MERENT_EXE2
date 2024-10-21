using Application.Commons;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.ComboDTOs;
using Application.ViewModels.ProductDTOs;
using Application.ViewModels.ProductOrderDetailDTOs;
using Application.ViewModels.ProductOrderDTOs;
using Application.ViewModels.RoleDTOs;
using Application.ViewModels.ServiceDTOs;
using Application.ViewModels.StoreDTOs;
using Application.ViewModels.WalletDTOs;
using AutoMapper;
using Domain.Entites;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            // Authentication
            CreateMap<User, AuthenAccountDTO>().ReverseMap();
            CreateMap<User, RegisterAccountDTO>().ReverseMap();

            //view
            CreateMap<User, AccountDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Combo, ComboDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<User, UserDetailsModel>().ReverseMap();
            CreateMap<Role, RoleInfoModel>().ReverseMap();

            CreateMap<ProductOrder, ProductOrderDTO>().ReverseMap();
            CreateMap<ProductOrderDetails, PODetailDTO>().ReverseMap();

            CreateMap<Wallet, WalletDTO>().ReverseMap();
            //create
            CreateMap<User, AccountAddDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Combo, ComboCreateDTO>().ReverseMap();
            CreateMap<Role, RoleCreateDTO>().ReverseMap();
            CreateMap<Service, ServiceCreateDTO>().ReverseMap();
            CreateMap<Store, StoreCreateDTO>().ReverseMap();

            CreateMap<ProductOrder, ProductOrderCreateDTO>().ReverseMap();
            CreateMap<ProductOrderDetails, PODetailCreateDTO>().ReverseMap();

            CreateMap<Wallet, WalletCreateDTO>().ReverseMap();
            //update
            CreateMap<User, AccountUpdateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Combo, ComboUpdateDTOs>().ReverseMap();
            CreateMap<Role, RoleUpdateDTO>().ReverseMap();
            CreateMap<Service, ServiceUpdateDTO>().ReverseMap();
            CreateMap<Store, StoreUpdateDTO>().ReverseMap();
            CreateMap<Product, UploadImageDTO>().ReverseMap();
           

            CreateMap<ProductOrder, ProductOrderUpdateDTO>().ReverseMap();
            CreateMap<ProductOrderDetails, PODetailUpdateDTO>().ReverseMap();

            CreateMap<Wallet, WalletUpdateDTO>().ReverseMap();
            //Pagination
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
        }
    }
}
