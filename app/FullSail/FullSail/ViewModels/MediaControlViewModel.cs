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
        public ICommand EscCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputBackAsync(); });
        public ICommand UpCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputUpAsync(); });
        public ICommand OSDCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.ShowOSD(); });
        public ICommand LeftCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputLeftAsync(); });
        public ICommand EnterCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputSelectAsync(); });
        public ICommand RightCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputRightAsync(); });
        public ICommand DownCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.InputDownAsync(); });
        public ICommand TogglePlayPauseCommand => new Command<Button>(async (Button button) => { await KodiClientSingleton.TogglePlayPausePlayerAsync(); });
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
    }
}
