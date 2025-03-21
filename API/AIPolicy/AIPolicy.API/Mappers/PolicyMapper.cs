using AIPolicy.Application.InputModel;
using AIPolicy.Core.Entity;
using AutoMapper;

namespace AIPolicy.API.Mappers;

public class PolicyMapper : Profile
{
    public PolicyMapper()
    {
        CreateMap<PolicyInputModel, Policy>();

    }
}
