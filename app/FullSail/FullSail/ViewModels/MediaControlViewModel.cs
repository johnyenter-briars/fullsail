using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels
{
    internal class MediaControlViewModel : BaseViewModel
    {
        public ICommand ButtonClickedCommand => new Command<string>(async (string buttonName) =>
        {
            switch (buttonName)
            {
                case "Esc":
                    await KodiClientSingleton.InputBackAsync();
                    break;
                case "OSD":
                    await KodiClientSingleton.ShowOSD();
                    break;
                case "Up":
                    await KodiClientSingleton.InputUpAsync();
                    break;
                case "Down":
                    await KodiClientSingleton.InputDownAsync();
                    break;
                case "Left":
                    await KodiClientSingleton.InputLeftAsync();
                    break;
                case "Right":
                    await KodiClientSingleton.InputRightAsync();
                    break;
                case "Enter":
                    await KodiClientSingleton.InputSelectAsync();
                    break;

                default:
                    throw new ApplicationException($"Unknown button value: {buttonName}");
            }
        });
    }
}
