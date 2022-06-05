using Catalog.Shared.HttpClients.Catalog;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;
using Enumerations = Catalog.Shared.Enumerations;

namespace Catalog.Web.WebAssembly.Pages.Admin.Trainer;

public partial class Profile
{
    [Inject]
    public ITrainerClient TrainerClient { get; set; } = null!;

    [Inject]
    public IToaster Toaster { get; set; } = null!;

    public string? Name { get; set; }

    public EditTrainerProfileCommand? EditProfileRequest { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var profile = await TrainerClient.GetTrainerProfileByIdAsync(Guid.Parse("3D915EC8-45A9-47CC-9BE7-765473BD29F3"));
        Name = $"{profile.Firstname} {profile.Lastname}";
        EditProfileRequest = ToRequest(profile);
    }

    private EditTrainerProfileCommand ToRequest(TrainerProfileDto profile)
    {
        return new EditTrainerProfileCommand()
        {
            Bio            = profile.Bio,
            Id             = profile.Id,
            Email          = profile.Email,
            SocialNetworks = ToSocialNetworks(profile),
            Profession     = profile.Profession
        };
    }

    public async Task SubmitAsync()
    {
        try
        {
            var profile = await TrainerClient.EditTrainerProfileByIdAsync(EditProfileRequest);
            EditProfileRequest = ToRequest(profile);
            Toaster.Success("Profile updated");
        }
        catch (Exception e)
        {
            Toaster.Error("An error occurred during the update, please try again.");
            Console.WriteLine(e.Message);
        }
    }

    private List<SocialNetworkAccountDto> ToSocialNetworks(TrainerProfileDto profile)
    {
        var socialNetworks = new List<SocialNetworkAccountDto>();
        foreach (var socialNetwork in Enumerations.Trainer.SocialNetwork.List.OrderBy(socialNetwork => socialNetwork))
        {
            var socialNetworkDto = new SocialNetworkAccountDto()
            {
                SocialNetworkId = socialNetwork
            };

            var existingNetwork = profile.SocialNetworks.FirstOrDefault(social => social.SocialNetworkId == socialNetwork);
            if (existingNetwork is not null)
            {
                socialNetworkDto.Url = existingNetwork.Url;
            }

            socialNetworks.Add(socialNetworkDto);
        }

        return socialNetworks;
    }
}
