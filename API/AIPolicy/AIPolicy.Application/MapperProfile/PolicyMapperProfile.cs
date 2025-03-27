using AIPolicy.Application.InputModel;
using AIPolicy.Application.ViewModel;
using AIPolicy.Core.Entity;
using AutoMapper;

namespace AIPolicy.Application.Mapper;

public class PolicyMapperProfile : Profile
{
    public PolicyMapperProfile()
    {
        CreateMap<PolicyInputModel, Policy>().ReverseMap();
        CreateMap<TriggerInputModel, Trigger>().ReverseMap();
        CreateMap<ConditionInputModel, Condition>().ReverseMap();

        CreateMap<Policy, PolicyViewModel>().ReverseMap();
        CreateMap<Trigger, TriggerViewModel>().ReverseMap();
        CreateMap<Condition, ConditionViewModel>().ReverseMap();
    }
}
