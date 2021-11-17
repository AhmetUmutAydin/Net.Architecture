﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Net.Architecture.Entities.BaseEntities;
using Net.Architecture.Entities.Concrete.Common;

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

        //#ToDo:İmplementasyon yapılacak queryable için
        public static TDestination DbToDto<TDestination>(this IQueryable queryable) where TDestination : IDTO
        {
            throw new NotImplementedException("Not İmpelemented");
            //return queryable.Adapt<TDestination>();
        }

        public static void AddMapsterConfigurations(this IServiceCollection services)
        {
            TypeAdapterConfig<DomainEnum, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => src.Description);
            TypeAdapterConfig<City, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => src.Name);
            TypeAdapterConfig<District, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => src.Name);
            TypeAdapterConfig<Employee, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => $"{src.PersonalInformation.Name} {src.PersonalInformation.Surname}");
            TypeAdapterConfig<Member, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => $"{src.PersonalInformation.Name} {src.PersonalInformation.Surname}");
            TypeAdapterConfig<Branch, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => $"{src.Name}");
            TypeAdapterConfig<PersonalInformation, DropdownDto>.NewConfig().Map(dest => dest.Value, src => src.Id).Map(dest => dest.Text, src => $"{src.Name} {src.Surname}");
            TypeAdapterConfig<BalanceDto, EmployeeBalance>.NewConfig().Map(dest => dest.EmployeeId, src => src.DeciderId);
            TypeAdapterConfig<BalanceDto, MemberBalance>.NewConfig().Map(dest => dest.MemberId, src => src.DeciderId);
        }
    }
}
