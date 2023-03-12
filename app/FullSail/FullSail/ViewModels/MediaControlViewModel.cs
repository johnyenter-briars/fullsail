using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels
{
    internal class MediaControlViewModel : BaseViewModel
    {
        public ICommand EscCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputBackAsync(); });
        public ICommand UpCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputUpAsync(); });
        public ICommand OSDCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.ShowOSD(); });
        public ICommand LeftCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputLeftAsync(); });
        public ICommand EnterCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputSelectAsync(); });
        public ICommand RightCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputRightAsync(); });
        public ICommand DownCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputDownAsync(); });
        public ICommand TogglePlayPauseCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.TogglePlayPausePlayerAsync(); });
        public ICommand RebootCommand => new Command<Button>(async (Button button) =>
        {
            if (await AlertServiceSingleton.ShowConfirmationAsync("Reboot confirmation", "Are you sure you want to reboot?"))
            {
                await KodiClientSingleton.PowerReboot();
            }
        });
        public ICommand PowerOffCommand => new Command<Button>(async (Button button) =>
        {
            if (await AlertServiceSingleton.ShowConfirmationAsync("Power confirmation", "Are you sure you want to power down?"))
            {
                await KodiClientSingleton.PowerOff();
            }
        });
        public ICommand RewindCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.SetPlayerSpeed(DecSpeed()); });
        public ICommand FastFowardCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.SetPlayerSpeed(IncSpeed()); });
        private string searchText = "";

        public string SearchText
        {
            get { return searchText; }
            set { SetProperty(ref searchText, value); }
        }
        public ICommand EnterTextCommand => new Command(async () =>
        {
            await KodiClientSingleton.InputSendText(searchText);
            SearchText = "";
        });
        public ICommand EnterTextChangedCommand => new Command<string>(async (string e) =>
        {
            await KodiClientSingleton.InputText(searchText);
        });
        public async Task SetFastFoward()
        {

        }
        public async Task SetRewind()
        {
            await KodiClientSingleton.SetPlayerSpeed(DecSpeed());
        }
        private int currSpeed = 0;
        private int MAX_FF_SPEED = 8;
        private int MAX_RR_SPEED = -8;

        public void SetSpeed(int s)
        {
            currSpeed = s;
        }

        public int IncSpeed()
        {
            if (currSpeed <= 1)
            {
                currSpeed = 2;
            }
            else if (currSpeed != MAX_FF_SPEED)
            {
                currSpeed += 2;
            }

            return currSpeed;
        }

        public int DecSpeed()
        {
            if (currSpeed >= 1)
            {
                currSpeed = -2;
            }
            else if (currSpeed != MAX_RR_SPEED)
            {
                currSpeed -= 2;
            }

            return currSpeed;
        }

        public int GetSpeed()
        {
            return currSpeed;
        }
    }
}
