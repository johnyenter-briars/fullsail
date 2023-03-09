using AlohaKit.Animations;
using FullSail.ViewModels;

namespace FullSail.Views;

public partial class RunningTorrentsPage : ContentPage
{
    public RunningTorrentsPage()
    {
        InitializeComponent();

        BindingContext = new RunningTorrentsViewModel();

    }
    protected override void OnAppearing()
    {
        Task.Run(async () =>
        {
            var bc = (RunningTorrentsViewModel)BindingContext;

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