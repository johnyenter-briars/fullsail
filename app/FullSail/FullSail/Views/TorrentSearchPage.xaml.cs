using AlohaKit.Animations;
using FullSail.ViewModels;
using System.Diagnostics;

namespace FullSail.Views;

public partial class TorrentSearchPage : ContentPage
{
    public TorrentSearchPage()
    {
        InitializeComponent();

        BindingContext = new TorrentSearchViewModel();
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