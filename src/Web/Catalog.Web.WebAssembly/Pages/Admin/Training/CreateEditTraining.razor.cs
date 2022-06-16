using System.Net;
using Ardalis.SmartEnum;
using Catalog.Shared.Enumerations.Training;
using Catalog.Shared.HttpClients.Catalog;
using Catalog.Web.WebAssembly.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sotsera.Blazor.Toaster;
using Topic = Catalog.Shared.Enumerations.Training.Topic;

namespace Catalog.Web.WebAssembly.Pages.Admin.Training;

public partial class CreateEditTraining : ComponentBase
{
    public EditTrainingCommand Request { get; set; } = new();

    [Parameter]
    public Guid? Id { get; set; }

    [Inject]
    public ITrainingClient TrainingClient { get; private set; } = default!;

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; private set; } = default!;

    [Inject]
    public IToaster Toaster { get; set; } = default!;

    protected string? Title { get; set; }

    protected IEnumerable<string>? ErrorMessages { get; private set; }

    protected override void OnInitialized()
    {
        InitCollections();
    }

    private void InitCollections()
    {
        Request.Topics            = new HashSet<int>();
        Request.VatJustifications = new HashSet<int>();
        Request.Audiences         = new HashSet<int>();
        Request.Attendances       = new HashSet<int>();
    }

    protected override async Task OnInitializedAsync()
    {
        if (Id != default)
        {
            try
            {
                await FetchTrainingDataAsync();
            }
            catch (ApiException exception)
            {
                await HandleExceptionOnFetchingTrainerAsync(exception);
            }
            catch (Exception)
            {
                Id = null;
                Toaster.Error("Training sheet could not be retrieved. Please try again later.");
            }
        }

        SetTitle();
    }

    private async Task FetchTrainingDataAsync()
    {
        var training              = await TrainingClient.TrainingByIdWithDetailsAsync(Id!.Value);
        Request.Title             = training.Title;
        Request.Description       = training.Description;
        Request.Goal              = training.Goal;
        Request.Attendances       = training.Attendances.ToHashSet();
        Request.VatJustifications = training.VatJustifications.ToHashSet();
        Request.Audiences         = training.Audiences.ToHashSet();
        Request.Topics            = training.Topics.ToHashSet();
        Request.Id                = training.Id;
    }

    private async Task SubmitAsync()
    {
        try
        {
            if (Id == default)
            {
                await TrainingClient.CreateTrainingAsync(ConvertRequestForTrainingCreation());
            }
            else
            {
                await TrainingClient.EditTrainingAsync(Request);
            }

            ShowToast();
            NavigationManager.NavigateTo("/Admin/mytrainings");
        }
        catch (Exception exception)
        {
            await HandleExceptionsOnSubmitAsync(exception);
        }
    }

    private void ShowToast()
    {
        var message = Id == default ? "Training created with success" : "Training updated with success";
        Toaster.Success(message);
    }

    private async Task HandleExceptionOnFetchingTrainerAsync(ApiException exception)
    {
        if (exception.HashErrorResponse())
        {
            var response = exception.ErrorResponse();
            if (response!.Status == (int) HttpStatusCode.NotFound)
            {
                Id = null;
            }
        }
        else
        {
            Id = null;
            Toaster.Error("An unexpected exception occurred, please try later");
        }

        await ScrollToTopAsync();
    }

    private async Task HandleExceptionsOnSubmitAsync(Exception exception)
    {
        if (exception is ApiException<ErrorResponse> apiExceptionWithResponse)
        {
            ErrorMessages = apiExceptionWithResponse.Result.Errors.Select(error => error.ErrorMessage);
        }
        else
        {
            Toaster.Error("An unexpected exception occurred, please try later");
            Console.WriteLine(exception.StackTrace + "\n" + exception.Message);
        }

        await ScrollToTopAsync();
    }

    private void SetTitle()
    {
        Title ??= Id == default ? "New training" : "Edition of training " + Request.Title;
    }

    private void SelectCheckbox<T>(SmartEnum<T> @enum, ICollection<int> set, ChangeEventArgs changeEventArgs) where T : SmartEnum<T, int>
    {
        var isSelected = (bool) changeEventArgs.Value!;

        if (isSelected)
        {
            set.Add(@enum.Value);
        }
        else
        {
            set.Remove(@enum.Value);
        }
    }

    private void OnAttendanceSelected(Attendance attendance, ChangeEventArgs changeEventArgs) => SelectCheckbox(attendance, Request.Attendances, changeEventArgs);

    private void OnAudienceSelected(Audience audience, ChangeEventArgs changeEventArgs) => SelectCheckbox(audience, Request.Audiences, changeEventArgs);

    private void OnTopicSelected(Topic topic, ChangeEventArgs changeEventArgs) => SelectCheckbox(topic, Request.Topics, changeEventArgs);

    private void OnVatJustificationSelected(VatJustification vatJustification, ChangeEventArgs changeEventArgs) => SelectCheckbox(vatJustification, Request.VatJustifications, changeEventArgs);

    public CreateTrainingCommand ConvertRequestForTrainingCreation()
    {
        return new CreateTrainingCommand()
        {
            Description       = Request.Description,
            Topics            = Request.Topics,
            Attendances       = Request.Attendances,
            VatJustifications = Request.VatJustifications,
            Goal              = Request.Goal,
            Audiences         = Request.Audiences,
            Title             = Request.Title
        };
    }

    private async Task ScrollToTopAsync() => await JsRuntime.InvokeVoidAsync("scrollToTop");
}
