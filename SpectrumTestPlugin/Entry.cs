using Spectrum.API.Interfaces.Plugins;
using Spectrum.API.Interfaces.Systems;
using SpectrumTestPlugin.UI;

namespace SpectrumTestPlugin
{
    public class Entry : IPlugin
    {
        public void Initialize(IManager manager, string ipcIdentifier)
        {
            Events.Scene.LoadFinish.Subscribe((data) =>
            {
                if (data.sceneName == "MainMenu")
                {
                    Menu.Create<SpectrumSettingsMenu>(manager, "SpectrumSettingsObject", "OptionsFrontRoot", "MainMenuFrontRoot");
                }
            });

            Events.Game.ModeInitialized.Subscribe((data) =>
            {
                Menu.Create<SpectrumSettingsMenu>(manager, "SpectrumSettingsObject", "OptionsFrontRoot(Clone)", "PauseMenuRoot");
            });
        }
    }
}
