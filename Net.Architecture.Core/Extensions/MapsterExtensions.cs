using System.Collections.Generic;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Net.Architecture.Entities.BaseEntities;
using Net.Architecture.Entities.Concrete.Common;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Core.Extensions
{
    public static class MapsterExtensions
    {
        public static TDestination To<TDestination>(this object source)
        {
            return source.Adapt<TDestination>();
        }
        public static TDestination ToDto<TDestination>(this IEntity entity) where TDestination : IDTO
        {
            return entity.Adapt<TDestination>();
        }
        public static IEnumerable<TDestination> ToDtos<TDestination>(this IEnumerable<IEntity> entities) where TDestination : IDTO
        {
            return entities.Adapt<IEnumerable<TDestination>>();
        }
        public static TDestination ToEntity<TDestination>(this IDTO dto) where TDestination : IEntity
        {
            return dto.Adapt<TDestination>();
        }
        public static IEnumerable<TDestination> ToEntities<TDestination>(this IEnumerable<IDTO> dtos) where TDestination : IEntity
        {
            return dtos.Adapt<IEnumerable<TDestination>>();
        }

        public static TDestination FillDto<TDestination>(this TDestination dto, BaseEntity entity) where TDestination : DtoBase
        {
            dto.Id = entity.Id;
            return dto;
        }

        public static void AddMapsterConfigurations(this IServiceCollection services)
        {
            TypeAdapterConfig<DomainEnum, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => src.Description);
            TypeAdapterConfig<City, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => src.Name);
            TypeAdapterConfig<District, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => src.Name);
            TypeAdapterConfig<PersonalInformation, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => $"{src.Name} {src.Surname}");
        }
    }
}
