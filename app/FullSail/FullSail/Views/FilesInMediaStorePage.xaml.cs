using AlohaKit.Animations;
using CommunityToolkit.Maui.Views;
using FullSail.ViewModels;

namespace FullSail.Views;

public partial class FilesInMediaStorePage : ContentPage
{
    public FilesInMediaStorePage()
    {
        InitializeComponent();
        BindingContext = new FilesInMediaStoreViewModel();
    }
    protected override void OnAppearing()
    {
        Task.Run(async () =>
        {
            var bc = (FilesInMediaStoreViewModel)BindingContext;
            await bc.Refresh();
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