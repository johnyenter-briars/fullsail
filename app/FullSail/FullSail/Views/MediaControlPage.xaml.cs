using AlohaKit.Animations;
using FullSail.ViewModels;
using System.Diagnostics;

namespace FullSail.Views;

public partial class MediaControlPage : ContentPage
{
    private KodiClient kodiClient;
    IDispatcherTimer timer;
    Stopwatch stopwatch = new Stopwatch();
    public MediaControlPage()
    {
        InitializeComponent();

        BindingContext = new MediaControlViewModel();

        kodiClient = DependencyService.Get<KodiClient>();

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(16);
        //timer.Tick += (s, e) =>
        //{
        //    label.Rotation = 360 * (stopwatch.Elapsed.TotalSeconds % 1);
        //};
    }
    //void OnFastFowardButtonPressed(object sender, EventArgs args)
    //{
    //    var viewModel = (MediaControlViewModel)BindingContext;
    //    Task.Run(async () =>
    //    {
    //        await viewModel.SetFastFoward();
    //    });
    //}

    //void OnFastForwardButtonReleased(object sender, EventArgs args)
    //{
    //    var viewModel = (MediaControlViewModel)BindingContext;
    //    viewModel.TogglePlayPauseCommand.Execute(null);
    //}
    //void OnRewindButtonPressed(object sender, EventArgs args)
    //{
    //    var viewModel = (MediaControlViewModel)BindingContext;
    //    Task.Run(async () =>
    //    {
    //        await viewModel.SetRewind();
    //    });
    //}

    //void OnRewindButtonReleased(object sender, EventArgs args)
    //{
    //    var viewModel = (MediaControlViewModel)BindingContext;
    //    await viewModel.TogglePlayPauseCommand();
    //}

    private void VolumeSlider_Changed(object sender, ValueChangedEventArgs e)
    {
        var slider = (Slider)sender;
        double distanceFromMin = (slider.Value - slider.Minimum);
        double sliderRange = (slider.Maximum - slider.Minimum);
        double sliderPercent = 100 * (distanceFromMin / sliderRange);

        int sliderPercentAsint = (int)sliderPercent;

        Task.Run(async () => await kodiClient.SetVolumeAsync(sliderPercentAsint));

    }
    private void SeekSlider_Changed(object sender, ValueChangedEventArgs e)
    {
        var slider = (Slider)sender;
        double distanceFromMin = (slider.Value - slider.Minimum);
        double sliderRange = (slider.Maximum - slider.Minimum);
        double sliderPercent = 100 * (distanceFromMin / sliderRange);

        int sliderPercentAsint = (int)sliderPercent;

        Task.Run(async () => await kodiClient.SeekPlayerAsync(sliderPercentAsint));
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