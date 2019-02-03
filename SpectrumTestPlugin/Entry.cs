using Spectrum.API.Interfaces.Plugins;
using Spectrum.API.Interfaces.Systems;
using SpectrumTestPlugin.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

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
                    CreateMenu(manager, "SpectrumSettingsObject", "OptionsFrontRoot", "MainMenuFrontRoot");
                }
            });

            Events.Game.ModeInitialized.Subscribe((data) =>
            {
                CreateMenu(manager, "SpectrumSettingsObject", "OptionsFrontRoot(Clone)", "PauseMenuRoot");
            });
        }

        private void CreateMenu(IManager manager, string settingsObjectName, string optionsFrontRootName, string mainMenuFrontRootName)
        {
            var spectrumSettingsObject = new GameObject(settingsObjectName);
            var menuController = spectrumSettingsObject.AddComponent<SpectrumSettingsMenu>();
            menuController.SetManager(manager);

            var optionsLogic = Util.FindByName(optionsFrontRootName).GetComponent<OptionsMenuLogic>();
            var options = new List<OptionsSubmenu>();
            options.AddRange(optionsLogic.subMenus_);
            options.Add(menuController);
            optionsLogic.subMenus_ = options.ToArray();

            var mainMenuLogic = Util.FindByName(mainMenuFrontRootName).GetComponent<MainMenuLogic>();
            List<MenuButtonList.ButtonInfo> buttonInfos = mainMenuLogic.optionsButtons_.GetButtonInfos(optionsLogic, false);
            mainMenuLogic.optionsButtons_.Init(buttonInfos);
        }
    }
}
