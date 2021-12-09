using AutoMapper;
using LojaVirtual.Models.ProdutoAgregador;

namespace LojaVirtual.Libraries.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoItemCarrinho>();
        }
    }
}
