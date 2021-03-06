﻿using ARK_Server_Manager.Lib;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ARK_Server_Manager.Lib.ViewModel;
using WPFSharp.Globalizer;

namespace ARK_Server_Manager
{
    public enum ServerSettingsCustomLevelsAction
    {
        ExportPlayerLevels,
        ImportPlayerLevels,
        UpdatePlayerXPCap,
        ExportDinoLevels,
        ImportDinoLevels,
        UpdateDinoXPCap,
    }

    public enum ServerSettingsResetAction
    {
        // Sections
        AdministrationSection,
        RulesSection,
        ChatAndNotificationsSection,
        HudAndVisualsSection,
        PlayerSettingsSection,
        DinoSettingsSection,
        EnvironmentSection,
        StructuresSection,
        EngramsSection,
        CustomLevelsSection,
        SOTFSection,

        // Properties
        MapNameProperty,
        PlayerMaxXpProperty,
        DinoMaxXpProperty,
        PlayerPerLevelStatMultipliers,
        DinoWildPerLevelStatMultipliers,
        DinoTamedPerLevelStatMultipliers,
        DinoTamedAddPerLevelStatMultipliers,
        DinoTamedAffinityPerLevelStatMultipliers,
    }

    /// <summary>
    /// Interaction logic for ServerSettings.xaml
    /// </summary>
    partial class ServerSettingsControl : UserControl
    {
        private GlobalizedApplication _globalizedApplication = GlobalizedApplication.Instance;

        // Using a DependencyProperty as the backing store for ServerManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServerManagerProperty = DependencyProperty.Register(nameof(ServerManager), typeof(ServerManager), typeof(ServerSettingsControl), new PropertyMetadata(null));
        public static readonly DependencyProperty SettingsProperty = DependencyProperty.Register(nameof(Settings), typeof(ServerProfile), typeof(ServerSettingsControl));
        public static readonly DependencyProperty RuntimeProperty = DependencyProperty.Register(nameof(Runtime), typeof(ServerRuntime), typeof(ServerSettingsControl));
        public static readonly DependencyProperty NetworkInterfacesProperty = DependencyProperty.Register(nameof(NetworkInterfaces), typeof(List<NetworkAdapterEntry>), typeof(ServerSettingsControl), new PropertyMetadata(new List<NetworkAdapterEntry>()));
        public static readonly DependencyProperty ServerProperty = DependencyProperty.Register(nameof(Server), typeof(Server), typeof(ServerSettingsControl), new PropertyMetadata(null, ServerPropertyChanged));
        public static readonly DependencyProperty CurrentConfigProperty = DependencyProperty.Register(nameof(CurrentConfig), typeof(Config), typeof(ServerSettingsControl));
        public static readonly DependencyProperty IsAdministratorProperty = DependencyProperty.Register(nameof(IsAdministrator), typeof(bool), typeof(ServerSettingsControl), new PropertyMetadata(false));
        public static readonly DependencyProperty DinoSettingsProperty = DependencyProperty.Register(nameof(BaseDinoSettings), typeof(DinoSettingsList), typeof(ServerSettingsControl), new PropertyMetadata(null));


        private static void ServerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ssc = (ServerSettingsControl)d;
            var oldserver = (Server)e.OldValue;
            var server = (Server)e.NewValue;
            if (server != null)
            {
                TaskUtils.RunOnUIThreadAsync(() =>
                    {
                        if (oldserver != null)
                        {
                            oldserver.Profile.Save();
                        }

                        ssc.Settings = server.Profile;
                        ssc.Runtime = server.Runtime;
                        ssc.ReinitializeNetworkAdapters();
                        ssc.RefreshDinoSettingsCombobox();
                    }).DoNotWait();
            }
        }

        private void GlobalizationManager_ResourceDictionaryChangedEvent(object source, ResourceDictionaryChangedEventArgs e)
        {
            this.Settings.DinoSettings.UpdateForLocalization();

            this.RefreshDinoSettingsCombobox();
            this.HarvestResourceItemAmountClassMultipliersListBox.Items.Refresh();
            this.EngramsOverrideListView.Items.Refresh();
        }


        CancellationTokenSource upgradeCancellationSource;


        public ServerManager ServerManager
        {
            get { return (ServerManager)GetValue(ServerManagerProperty); }
            set { SetValue(ServerManagerProperty, value); }
        }

        public Config CurrentConfig
        {
            get { return GetValue(CurrentConfigProperty) as Config; }
            set { SetValue(CurrentConfigProperty, value); }
        }

        public bool IsAdministrator
        {
            get { return (bool)GetValue(IsAdministratorProperty); }
            set { SetValue(IsAdministratorProperty, value); }
        }

        public Server Server
        {
            get { return (Server)GetValue(ServerProperty); }
            set { SetValue(ServerProperty, value); }
        }

        public ServerProfile Settings
        {
            get { return GetValue(SettingsProperty) as ServerProfile; }
            set { SetValue(SettingsProperty, value); }
        }

        public ServerRuntime Runtime
        {
            get { return GetValue(RuntimeProperty) as ServerRuntime; }
            set { SetValue(RuntimeProperty, value); }
        }

        public List<NetworkAdapterEntry> NetworkInterfaces
        {
            get { return (List<NetworkAdapterEntry>)GetValue(NetworkInterfacesProperty); }
            set { SetValue(NetworkInterfacesProperty, value); }
        }

        public DinoSettingsList BaseDinoSettings
        {
            get { return (DinoSettingsList)GetValue(DinoSettingsProperty); }
            set { SetValue(DinoSettingsProperty, value); }
        }

        public ServerSettingsControl()
        {
            this.CurrentConfig = Config.Default;
            InitializeComponent();
            WindowUtils.RemoveDefaultResourceDictionary(this);

            this.ServerManager = ServerManager.Instance;
            this.IsAdministrator = SecurityUtils.IsAdministrator();

            this.BaseDinoSettings = new DinoSettingsList();

            // hook into the language change event
            GlobalizedApplication.Instance.GlobalizationManager.ResourceDictionaryChangedEvent += GlobalizationManager_ResourceDictionaryChangedEvent;
        }

        private void ReinitializeNetworkAdapters()
        {
            var adapters = NetworkUtils.GetAvailableIPV4NetworkAdapters();

            //
            // Filter out self-assigned addresses
            //
            adapters.RemoveAll(a => a.IPAddress.StartsWith("169.254."));
            adapters.Insert(0, new NetworkAdapterEntry(String.Empty, _globalizedApplication.GetResourceString("ServerSettings_LocalIPArkChooseLabel")));
            var savedServerIp = this.Settings.ServerIP;
            this.NetworkInterfaces = adapters;
            this.Settings.ServerIP = savedServerIp;


            //
            // If there isn't already an adapter assigned, pick one
            //
            var preferredIP = NetworkUtils.GetPreferredIP(adapters);
            preferredIP.Description = _globalizedApplication.GetResourceString("ServerSettings_LocalIPRecommendedLabel") + " " + preferredIP.Description;
            if (String.IsNullOrWhiteSpace(this.Settings.ServerIP))
            {
                // removed to enforce the 'Let ARK choose' option.
                //if (preferredIP != null)
                //{
                //    this.Settings.ServerIP = preferredIP.IPAddress;
                //}
            }
            else if (adapters.FirstOrDefault(a => String.Equals(a.IPAddress, this.Settings.ServerIP, StringComparison.OrdinalIgnoreCase)) == null)
            {
                MessageBox.Show(
                    String.Format(_globalizedApplication.GetResourceString("ServerSettings_LocalIP_ErrorLabel"), this.Settings.ServerIP),
                    _globalizedApplication.GetResourceString("ServerSettings_LocalIP_ErrorTitle"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void RefreshDinoSettingsCombobox()
        {
            this.BaseDinoSettings = this.Settings.DinoSettings.Clone();
            this.DinoSettingsGrid.Items.Refresh();
        }

        private async void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            switch (this.Runtime.Status)
            {
                case ServerRuntime.ServerStatus.Stopped:
                case ServerRuntime.ServerStatus.Uninstalled:
                    break;

                case ServerRuntime.ServerStatus.Running:
                case ServerRuntime.ServerStatus.Initializing:
                    var result = MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_UpgradeServer_RunningLabel"), _globalizedApplication.GetResourceString("ServerSettings_UpgradeServer_RunningTitle"), MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }

                    break;

                case ServerRuntime.ServerStatus.Updating:
                    upgradeCancellationSource.Cancel();
                    upgradeCancellationSource = null;
                    return;
            }

            this.upgradeCancellationSource = new CancellationTokenSource();
            await this.Server.UpgradeAsync(upgradeCancellationSource.Token, validate: true);
        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            switch (this.Runtime.Status)
            {
                case ServerRuntime.ServerStatus.Initializing:
                case ServerRuntime.ServerStatus.Running:
                    var result = MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_StartServer_RunningLabel"), _globalizedApplication.GetResourceString("ServerSettings_StartServer_RunningTitle"), MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }

                    await this.Server.StopAsync();
                    break;

                case ServerRuntime.ServerStatus.Stopped:
                    this.Settings.Save();
                    await this.Server.StartAsync();
                    break;
            }
        }

        private void SelectInstallDirectory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = _globalizedApplication.GetResourceString("ServerSettings_InstallServer_Title");
            if (!String.IsNullOrWhiteSpace(Settings.InstallDirectory))
            {
                dialog.InitialDirectory = Settings.InstallDirectory;
            }

            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                Settings.InstallDirectory = dialog.FileName;
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.EnsureFileExists = true;
            dialog.Multiselect = false;
            dialog.Title = _globalizedApplication.GetResourceString("ServerSettings_LoadConfig_Title");
            dialog.Filters.Add(new CommonFileDialogFilter("Profile", Config.Default.LoadProfileExtensionList));
            if (!Directory.Exists(Config.Default.ConfigDirectory))
            {
                System.IO.Directory.CreateDirectory(Config.Default.ConfigDirectory);
            }

            dialog.InitialDirectory = Config.Default.ConfigDirectory;
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                try
                {
                    this.Server.ImportFromPath(dialog.FileName);
                    this.Settings = this.Server.Profile;
                    this.Runtime = this.Server.Runtime;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(_globalizedApplication.GetResourceString("ServerSettings_LoadConfig_ErrorLabel"), dialog.FileName, ex.Message, ex.StackTrace), _globalizedApplication.GetResourceString("ServerSettings_LoadConfig_ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        // REVIEW: This is a sample Command implementation which replaces the original Save_Click command, for reference when refactoring.
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand<object>(
                    execute: (parameter) =>
                    {
                        // NOTE: This parameter is of type object and must be cast in most cases before use.
                        var settings = (Server)parameter;
                        if (settings.Profile.EnableAutoUpdate)
                        {
                            if (settings.Profile.SOTF_Enabled)
                            {
                                MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_Save_AutoUpdate_ErrorLabel"), _globalizedApplication.GetResourceString("ServerSettings_Save_AutoUpdate_ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Warning);
                                settings.Profile.EnableAutoUpdate = false;
                            }
                            else if (!Updater.IsServerCacheAutoUpdateEnabled)
                            {
                                var result = MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_Save_ServerCache_ErrorLabel1"), _globalizedApplication.GetResourceString("ServerSettings_Save_ServerCache_ErrorTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                                if (result == MessageBoxResult.Yes)
                                {
                                    var settingsWindow = new SettingsWindow();
                                    settingsWindow.ShowDialog();
                                }

                                // retest the server cache configuration
                                if (!Updater.IsServerCacheAutoUpdateEnabled)
                                {
                                    MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_Save_ServerCache_ErrorLabel2"), _globalizedApplication.GetResourceString("ServerSettings_Save_ServerCache_ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Warning);
                                    settings.Profile.EnableAutoUpdate = false;
                                }
                            }
                        }

                        settings.Profile.Save();

                        // NOTE: Ideally a command would not depend on this control object, so IsAdministrator would need to be some globally accessible value, much like Updater's properties are.  Then
                        //       command's implementation becomes context-free and we can move its implementation to a separate class of commands, and bind it in the Xaml using a StaticResource.
                        if (this.IsAdministrator)
                        {
                            if (!settings.Profile.UpdateAutoUpdateSettings())
                            {
                                MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_Save_UpdateSchedule_ErrorLabel"), _globalizedApplication.GetResourceString("ServerSettings_Save_UpdateSchedule_ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    },
                    canExecute: (parameter) =>
                    {
                        bool canSave = true;

                        // NOTE: Some logic if necessary.  If this return's false, the associated object to which this command is bound (like the Save button in this case) will be automatically disabled,
                        // eliminating any extra Xaml binding for the IsEnabled property.
                        return canSave;
                    }
                );
            }
        }

        private void CopyProfile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DeleteProfile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ShowCmd_Click(object sender, RoutedEventArgs e)
        {
            var cmdLine = new CommandLine(String.Format("{0} {1}", this.Runtime.GetServerExe(), this.Settings.GetServerArgs()));
            cmdLine.Owner = Window.GetWindow(this);
            cmdLine.ShowDialog();
        }

        private void RemovePlayerLevel_Click(object sender, RoutedEventArgs e)
        {
            if (this.Settings.PlayerLevels.Count == 1)
            {
                MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_CustomLevels_LastRemove_ErrorLabel"), _globalizedApplication.GetResourceString("ServerSettings_CustomLevels_LastRemove_ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                var level = ((Level)((Button)e.Source).DataContext);
                this.Settings.PlayerLevels.RemoveLevel(level);
            }
        }

        private void AddPlayerLevel_Click(object sender, RoutedEventArgs e)
        {
            var level = ((Level)((Button)e.Source).DataContext);
            this.Settings.PlayerLevels.AddNewLevel(level, Config.Default.CustomLevelXPIncrease_Player);
        }

        private void RemoveDinoLevel_Click(object sender, RoutedEventArgs e)
        {
            if (this.Settings.DinoLevels.Count == 1)
            {
                MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_CustomLevels_LastRemove_ErrorLabel"), _globalizedApplication.GetResourceString("ServerSettings_CustomLevels_LastRemove_ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                var level = ((Level)((Button)e.Source).DataContext);
                this.Settings.DinoLevels.RemoveLevel(level);
            }
        }

        private void AddDinoLevel_Click(object sender, RoutedEventArgs e)
        {
            var level = ((Level)((Button)e.Source).DataContext);
            this.Settings.DinoLevels.AddNewLevel(level, Config.Default.CustomLevelXPIncrease_Dino);
        }

        private void RemoveDinoSetting_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_DinoCustomization_DinoRemoveRecordLabel"), _globalizedApplication.GetResourceString("ServerSettings_DinoCustomization_DinoRemoveRecordTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            var dino = ((DinoSettings)((Button)e.Source).DataContext);
            if (!dino.KnownDino)
            {
                this.Settings.DinoSettings.Remove(dino);
                RefreshDinoSettingsCombobox();
            }
        }

        private void RemoveHarvestResource_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_Harvest_HarvestRemoveRecordLabel"), _globalizedApplication.GetResourceString("ServerSettings_Harvest_HarvestRemoveRecordTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            var resource = ((ResourceClassMultiplier)((Button)e.Source).DataContext);
            if (!resource.KnownResource)
                this.Settings.HarvestResourceItemAmountClassMultipliers.Remove(resource);
        }

        private void RemoveEngramOverride_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_EngramsOverride_EngramsRemoveRecordLabel"), _globalizedApplication.GetResourceString("ServerSettings_EngramsOverride_EngramsRemoveRecordTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            var engram = ((EngramEntry)((Button)e.Source).DataContext);
            if (!engram.KnownEngram)
                this.Settings.OverrideNamedEngramEntries.Remove(engram);
        }

        private void PlayerLevels_Recalculate(object sender, RoutedEventArgs e)
        {
            this.Settings.PlayerLevels.UpdateTotals();
            this.CustomPlayerLevelsView.Items.Refresh();
        }

        private void DinoLevels_Recalculate(object sender, RoutedEventArgs e)
        {
            this.Settings.DinoLevels.UpdateTotals();
            this.CustomDinoLevelsView.Items.Refresh();
        }

        private void RefreshLocalIPs_Click(object sender, RoutedEventArgs e)
        {
            ReinitializeNetworkAdapters();
        }

        private void DinoLevels_Clear(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_DinoLevels_ClearLabel"), _globalizedApplication.GetResourceString("ServerSettings_DinoLevels_ClearTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.ClearLevelProgression(ServerProfile.LevelProgression.Dino);
        }

        private void DinoLevels_Reset(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_DinoLevels_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_DinoLevels_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.ResetLevelProgressionToDefault(ServerProfile.LevelProgression.Dino);
        }

        private void DinoLevels_ResetOfficial(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_DinoLevels_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_DinoLevels_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.ResetLevelProgressionToOfficial(ServerProfile.LevelProgression.Dino);
        }

        private void PlayerLevels_Clear(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_PlayerLevels_ClearLabel"), _globalizedApplication.GetResourceString("ServerSettings_PlayerLevels_ClearTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.ClearLevelProgression(ServerProfile.LevelProgression.Player);
        }

        private void PlayerLevels_Reset(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_PlayerLevels_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_PlayerLevels_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.ResetLevelProgressionToDefault(ServerProfile.LevelProgression.Player);
        }

        private void PlayerLevels_ResetOfficial(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_PlayerLevels_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_PlayerLevels_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.ResetLevelProgressionToOfficial(ServerProfile.LevelProgression.Player);
        }

        private void DinoSpawn_Reset(object sender, RoutedEventArgs e)
        {
            this.Settings.DinoSpawnWeightMultipliers.Reset();
        }

        private void TamedDinoClassDamageMultipliers_Reset(object sender, RoutedEventArgs e)
        {
            this.Settings.TamedDinoClassDamageMultipliers.Reset();
        }

        private void TamedDinoClassResistanceMultipliers_Reset(object sender, RoutedEventArgs e)
        {
            this.Settings.TamedDinoClassResistanceMultipliers.Reset();
        }

        private void DinoClassDamageMultipliers_Reset(object sender, RoutedEventArgs e)
        {
            this.Settings.DinoClassDamageMultipliers.Reset();
        }

        private void DinoClassResistanceMultipliers_Reset(object sender, RoutedEventArgs e)
        {
            this.Settings.DinoClassResistanceMultipliers.Reset();
        }

        private void HarvestResourceItemAmountClassMultipliers_Reset(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_CustomHarvest_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_CustomHarvest_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.HarvestResourceItemAmountClassMultipliers.Reset();
        }

        private void OpenRCON_Click(object sender, RoutedEventArgs e)
        {
            var window = RCONWindow.GetRCONForServer(this.Server);
            window.Show();
            if (window.WindowState == WindowState.Minimized)
            {
                window.WindowState = WindowState.Normal;
            }

            window.Focus();
        }

        private void Engrams_Reset(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_EngramsOverride_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_EngramsOverride_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.OverrideNamedEngramEntries.Reset();
        }

        private void HelpSOTF_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Default.ArkSotfUrl);
        }

        private void PatchNotes_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.SOTF_Enabled)
                Process.Start(Config.Default.ArkSotF_PatchNotesUrl);
            else
                Process.Start(Config.Default.ArkSE_PatchNotesUrl);
        }

        private void TestUpdater_Click(object sender, RoutedEventArgs e)
        {
            if (!this.Settings.UpdateAutoUpdateSettings())
            {

            }
        }

        private void NeedAdmin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_AdminRequired_ErrorLabel"), _globalizedApplication.GetResourceString("ServerSettings_AdminRequired_ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DinoCustomization_Reset(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_DinoCustomization_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_DinoCustomization_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.DinoSettings.Reset();
            RefreshDinoSettingsCombobox();
        }

        private void MaxXPPlayer_Reset(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_PlayerMaxXP_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_PlayerMaxXP_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.ResetOverrideMaxExperiencePointsPlayer();
        }

        private void MaxXPDino_Reset(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_DinoMaxXP_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_DinoMaxXP_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            this.Settings.ResetOverrideMaxExperiencePointsDino();
        }

        private CommonFileDialog GetCustomLevelCommonFileDialog(ServerSettingsCustomLevelsAction action)
        {
            CommonFileDialog dialog = null;

            switch (action)
            {
                case ServerSettingsCustomLevelsAction.ExportDinoLevels:
                case ServerSettingsCustomLevelsAction.ExportPlayerLevels:
                    dialog = new CommonSaveFileDialog();
                    dialog.Title = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ExportDialogTitle");
                    dialog.DefaultExtension = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ExportDefaultExtension");
                    dialog.Filters.Add(new CommonFileDialogFilter(GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ExportFilterLabel"), GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ExportFilterExtension")));
                    break;

                case ServerSettingsCustomLevelsAction.ImportDinoLevels:
                case ServerSettingsCustomLevelsAction.ImportPlayerLevels:
                    dialog = new CommonOpenFileDialog();
                    dialog.Title = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ImportDialogTitle");
                    dialog.DefaultExtension = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ImportDefaultExtension");
                    dialog.Filters.Add(new CommonFileDialogFilter(GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ImportFilterLabel"), GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ImportFilterExtension")));
                    break;
            }

            return dialog;
        }

        public ICommand CustomLevelActionCommand
        {
            get
            {
                return new RelayCommand<ServerSettingsCustomLevelsAction>(
                    execute: (action) =>
                    {
                        var errorTitle = GlobalizedApplication.Instance.GetResourceString("Generic_ErrorLabel");

                        try
                        {
                            var dialog = GetCustomLevelCommonFileDialog(action);
                            var dialogValue = string.Empty;
                            if (dialog != null && dialog.ShowDialog() == CommonFileDialogResult.Ok)
                                dialogValue = dialog.FileName;

                            switch (action)
                            {
                                case ServerSettingsCustomLevelsAction.ExportDinoLevels:
                                    errorTitle = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ExportErrorTitle");

                                    this.Settings.ExportDinoLevels(dialogValue);
                                    break;

                                case ServerSettingsCustomLevelsAction.ImportDinoLevels:
                                    errorTitle = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ImportErrorTitle");

                                    this.Settings.ImportDinoLevels(dialogValue);
                                    break;

                                case ServerSettingsCustomLevelsAction.UpdateDinoXPCap:
                                    errorTitle = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_UpdateErrorTitle");

                                    this.Settings.UpdateOverrideMaxExperiencePointsDino();
                                    break;

                                case ServerSettingsCustomLevelsAction.ExportPlayerLevels:
                                    errorTitle = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ExportErrorTitle");

                                    this.Settings.ExportPlayerLevels(dialogValue);
                                    break;

                                case ServerSettingsCustomLevelsAction.ImportPlayerLevels:
                                    errorTitle = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_ImportErrorTitle");

                                    this.Settings.ImportPlayerLevels(dialogValue);
                                    break;

                                case ServerSettingsCustomLevelsAction.UpdatePlayerXPCap:
                                    errorTitle = GlobalizedApplication.Instance.GetResourceString("ServerSettings_CustomLevel_UpdateErrorTitle");

                                    this.Settings.UpdateOverrideMaxExperiencePointsPlayer();
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, errorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    },
                    canExecute: (action) => true
                );
            }
        }

        public ICommand ResetActionCommand
        {
            get
            {
                return new RelayCommand<ServerSettingsResetAction>(
                    execute: (action) =>
                    {
                        if (MessageBox.Show(_globalizedApplication.GetResourceString("ServerSettings_ResetLabel"), _globalizedApplication.GetResourceString("ServerSettings_ResetTitle"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;

                        switch (action)
                        {
                            // sections
                            case ServerSettingsResetAction.AdministrationSection:
                                this.Settings.ResetAdministrationSection();
                                break;

                            case ServerSettingsResetAction.ChatAndNotificationsSection:
                                this.Settings.ResetChatAndNotificationSection();
                                break;

                            case ServerSettingsResetAction.CustomLevelsSection:
                                this.Settings.ResetCustomLevelsSection();
                                break;

                            case ServerSettingsResetAction.DinoSettingsSection:
                                this.Settings.ResetDinoSettings();
                                RefreshDinoSettingsCombobox();
                                break;

                            case ServerSettingsResetAction.EngramsSection:
                                this.Settings.ResetEngramsSection();
                                break;

                            case ServerSettingsResetAction.EnvironmentSection:
                                this.Settings.ResetEnvironmentSection();
                                break;

                            case ServerSettingsResetAction.HudAndVisualsSection:
                                this.Settings.ResetHUDAndVisualsSection();
                                break;

                            case ServerSettingsResetAction.PlayerSettingsSection:
                                this.Settings.ResetPlayerSettings();
                                break;

                            case ServerSettingsResetAction.RulesSection:
                                this.Settings.ResetRulesSection();
                                break;

                            case ServerSettingsResetAction.SOTFSection:
                                this.Settings.ResetSOTFSection();
                                break;

                            case ServerSettingsResetAction.StructuresSection:
                                this.Settings.ResetStructuresSection();
                                break;

                            // Properties
                            case ServerSettingsResetAction.MapNameProperty:
                                this.Settings.ResetMapName();
                                break;

                            case ServerSettingsResetAction.PlayerMaxXpProperty:
                                this.Settings.ResetOverrideMaxExperiencePointsPlayer();
                                break;

                            case ServerSettingsResetAction.DinoMaxXpProperty:
                                this.Settings.ResetOverrideMaxExperiencePointsDino();
                                break;

                            case ServerSettingsResetAction.PlayerPerLevelStatMultipliers:
                                this.Settings.PerLevelStatsMultiplier_Player.Reset();
                                break;

                            case ServerSettingsResetAction.DinoWildPerLevelStatMultipliers:
                                this.Settings.PerLevelStatsMultiplier_DinoWild.Reset();
                                break;

                            case ServerSettingsResetAction.DinoTamedPerLevelStatMultipliers:
                                this.Settings.PerLevelStatsMultiplier_DinoTamed.Reset();
                                break;

                            case ServerSettingsResetAction.DinoTamedAddPerLevelStatMultipliers:
                                this.Settings.PerLevelStatsMultiplier_DinoTamed_Add.Reset();
                                break;

                            case ServerSettingsResetAction.DinoTamedAffinityPerLevelStatMultipliers:
                                this.Settings.PerLevelStatsMultiplier_DinoTamed_Affinity.Reset();
                                break;
                        }
                    },
                    canExecute: (action) => true
                );
            }
        }
    }
}
