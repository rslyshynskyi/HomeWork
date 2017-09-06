using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MatOrderingService.Domain;
using MatOrderingService.Models;

namespace MatOrderingService.Config.Mapping
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<Order, OrderInfo>()
                .ForMember(d => d.Status,
                    opt => opt.MapFrom(s => s.Status.ToString()));

            CreateMap<NewOrder, Order>();
            CreateMap<EditOrder, Order>();

            CreateMap<NewOrderData, OrderItem>();

            CreateMap<OrderItem, OrderItemInfo>()
                .ForMember(toItemInfo => toItemInfo.ProductCode, info => info.MapFrom(from => from.Product.Code))
                .ForMember(d => d.ProductName, info => info.MapFrom(s => s.Product.Name));
        }
    }
}
