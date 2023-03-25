using AlohaKit.Animations;
using FullSail.ViewModels;

namespace FullSail.Views;

public partial class HealthCheckPage : ContentPage
{
    public HealthCheckPage()
    {
        InitializeComponent();

        BindingContext = new HealthCheckViewModel();
    }
    protected override void OnAppearing()
    {
        Task.Run(async () =>
        {
            var bc = (HealthCheckViewModel)BindingContext;

            await bc?.Refresh();
        });
    }
    private void ScaleButton(object sender, EventArgs e)
    {
        if (sender is View view)
        {
            view.Animate(new StoryBoard(new List<AnimationBase>
              {
                 new ScaleToAnimation { Scale = 1.1, Duration = "150" },
                 new ScaleToAnimation { Scale = 1, Duration = "100" }
              }));
        }
    }
}