using FullSail.ViewModels;

namespace FullSail.Views;

public partial class MediaControlPage : ContentPage
{
    private KodiClient kodiClient;
    public MediaControlPage()
    {
        InitializeComponent();

        BindingContext = new MediaControlViewModel();

        kodiClient = DependencyService.Get<KodiClient>();
    }

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
}