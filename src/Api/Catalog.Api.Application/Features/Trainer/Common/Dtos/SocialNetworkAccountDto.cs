using Catalog.Api.Domain.Entities;
using Catalog.Api.Domain.Entities.TrainerAggregate;

namespace Catalog.Api.Application.Features.Trainer.Common.Dtos;

public class SocialNetworkAccountDto
{
    public int SocialNetworkId { get; set; }

    public string? Url { get; set; }
}

public static class SocialNetworkMapper
{
    public static SocialNetworkAccountDto ToDto(this TrainerSocialNetwork socialNetwork)
    {
        return new SocialNetworkAccountDto()
        {
            Url             = socialNetwork.Url,
            SocialNetworkId = socialNetwork.SocialNetwork
        };
    }

    public static List<SocialNetworkAccountDto> ToDtos(this IEnumerable<TrainerSocialNetwork> socialNetworks)
    {
        return socialNetworks.Select(network => network.ToDto()).ToList();
    }
}
