// Project:         EasyTimeScale mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2022 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    1/5/2022, 11:50 AM
// Last Edit:		6/6/2022, 9:30 AM
// Version:			1.00
// Special Thanks:  
// Modifier:			

using System;
using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using DaggerfallWorkshop.Game.Serialization;
using DaggerfallWorkshop.Game.Utility.ModSupport.ModSettings;
using DaggerfallWorkshop.Game.Utility;

namespace EasyTimeScale
{
    public class EasyTimeScaleMain : MonoBehaviour
    {
        static EasyTimeScaleMain instance;

        public static EasyTimeScaleMain Instance
        {
            get { return instance ?? (instance = FindObjectOfType<EasyTimeScaleMain>()); }
        }

        static Mod mod;

        // Options
        public static int TimeScaleSetting { get; set; }

        [Invoke(StateManager.StateTypes.Start, 0)]
        public static void Init(InitParams initParams)
        {
            mod = initParams.Mod;
            instance = new GameObject("EasyTimeScale").AddComponent<EasyTimeScaleMain>(); // Add script to the scene.

            mod.LoadSettingsCallback = LoadSettings; // To enable use of the "live settings changes" feature in-game.

            mod.IsReady = true;
        }

        private void Start()
        {
            Debug.Log("Begin mod init: Easy Time Scale");

            mod.LoadSettings();

            StartGameBehaviour.OnStartGame += ChangeTimeScale_OnStartGame;
            SaveLoadManager.OnLoad += ChangeTimeScale_OnSaveLoad;

            Debug.Log("Finished mod init: Easy Time Scale");
        }

        #region Settings

        static void LoadSettings(ModSettings modSettings, ModSettingsChange change)
        {
            TimeScaleSetting = mod.GetSettings().GetValue<int>("Options", "TimeScale");

            DaggerfallWorkshop.DaggerfallUnity.Instance.WorldTime.TimeScale = GetTimeScaleSetting();
            Debug.LogFormat("Time Scale set to, {0}. By EasyTimeScale mod.", GetTimeScaleSetting());
        }

        #endregion

        public static int GetTimeScaleSetting()
        {
            return TimeScaleSetting;
        }

        static void ChangeTimeScale_OnStartGame(object sender, EventArgs e)
        {
            DaggerfallWorkshop.DaggerfallUnity.Instance.WorldTime.TimeScale = GetTimeScaleSetting();
            Debug.LogFormat("Time Scale set to, {0}. By EasyTimeScale mod.", GetTimeScaleSetting());
        }

        static void ChangeTimeScale_OnSaveLoad(SaveData_v1 saveData)
        {
            DaggerfallWorkshop.DaggerfallUnity.Instance.WorldTime.TimeScale = GetTimeScaleSetting();
            Debug.LogFormat("Time Scale set to, {0}. By EasyTimeScale mod.", GetTimeScaleSetting());
        }
    }
}
