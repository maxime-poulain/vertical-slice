using Catalog.Shared.HttpClients.Catalog;
using Microsoft.AspNetCore.Components;

namespace Catalog.Web.WebAssembly.Pages.Admin.Training;

public partial class MyTrainings
{
    [Inject]
    public ITrainingClient TrainingClient { get; set; } = default!;

    public List<TrainingDto>? Trainings { get; set; }

    protected override async Task OnInitializedAsync()
    {
        //Trainings = await TrainingClient.TrainingForAdminPageByTrainerIdAsync(1);
        Trainings = await TrainingClient.AllTrainingsAsync();
    }

    private async Task DeleteTrainingAsync(TrainingDto training)
    {
        await TrainingClient.DeleteTrainingAsync(training.Id);
        Trainings!.Remove(training);
    }
}
