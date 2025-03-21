using AIPolicy.Application.InputModel;
using AIPolicy.Application.ViewModel;
using AIPolicy.Core.Entity;
using AutoMapper;

namespace AIPolicy.Application.Mapper;

public class PolicyMapperProfile : Profile
{
    public PolicyMapperProfile()
    {
        CreateMap<PolicyInputModel, Policy>();
        CreateMap<TriggerInputModel, Trigger>();
        CreateMap<ConditionInputModel, Condition>();

        CreateMap<Policy, PolicyViewModel>();
        CreateMap<Trigger, TriggerViewModel>();
        CreateMap<Condition, ConditionViewModel>();
    }
}
