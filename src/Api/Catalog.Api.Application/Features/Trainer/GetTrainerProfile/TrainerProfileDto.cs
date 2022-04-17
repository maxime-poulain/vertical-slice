using Catalog.Api.Application.Features.Trainer.Common.Dtos;

namespace Catalog.Api.Application.Features.Trainer.GetTrainerProfile;

public class TrainerProfileDto
{
    public int Id { get; init; }

    public string? Email { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Profession { get; set; }

    public string Bio { get; set; } = null!;

    public List<SocialNetworkAccountDto>? SocialNetworks { get; set; }
}
