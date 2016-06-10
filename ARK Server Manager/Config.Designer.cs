﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ARK_Server_Manager {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    public sealed partial class Config : global::System.Configuration.ApplicationSettingsBase {
        
        private static Config defaultInstance = ((Config)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Config())));
        
        public static Config Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.dropbox.com/s/a6v1obnqigu2bpu/version.txt?dl=1")]
        public string DefaultVersionCheckUrl {
            get {
                return ((string)(this["DefaultVersionCheckUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip")]
        public string SteamCmdUrl {
            get {
                return ((string)(this["SteamCmdUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SteamCMD.exe")]
        public string SteamCmdExe {
            get {
                return ((string)(this["SteamCmdExe"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SteamCMD")]
        public string SteamCmdDir {
            get {
                return ((string)(this["SteamCmdDir"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SteamCMD.zip")]
        public string SteamCmdZip {
            get {
                return ((string)(this["SteamCmdZip"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("+login anonymous +quit")]
        public string SteamCmdInstallArgs {
            get {
                return ((string)(this["SteamCmdInstallArgs"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ConfigDirectory {
            get {
                return ((string)(this["ConfigDirectory"]));
            }
            set {
                this["ConfigDirectory"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("+login anonymous +force_install_dir \"{0}\"  \"+app_update 376030 {1}\" +quit")]
        public string SteamCmdInstallServerArgsFormat {
            get {
                return ((string)(this["SteamCmdInstallServerArgsFormat"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Servers")]
        public string ServersInstallDir {
            get {
                return ((string)(this["ServersInstallDir"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Unnamed Server")]
        public string DefaultServerProfileName {
            get {
                return ((string)(this["DefaultServerProfileName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Unnamed Server")]
        public string DefaultServerName {
            get {
                return ((string)(this["DefaultServerName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ShooterGame\\Binaries\\Win64")]
        public string ServerBinaryRelativePath {
            get {
                return ((string)(this["ServerBinaryRelativePath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ShooterGame\\Saved\\Config\\WindowsServer")]
        public string ServerConfigRelativePath {
            get {
                return ((string)(this["ServerConfigRelativePath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ShooterGameServer.exe")]
        public string ServerExe {
            get {
                return ((string)(this["ServerExe"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("?QueryPort={0}")]
        public string ServerCommandLineArgsMatchFormat {
            get {
                return ((string)(this["ServerCommandLineArgsMatchFormat"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-nosteamclient -game -server -log")]
        public string ServerCommandLineStandardArgs {
            get {
                return ((string)(this["ServerCommandLineStandardArgs"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Profiles")]
        public string ProfilesDir {
            get {
                return ((string)(this["ProfilesDir"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".profile")]
        public string ProfileExtension {
            get {
                return ((string)(this["ProfileExtension"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TheIsland")]
        public string DefaultServerMap {
            get {
                return ((string)(this["DefaultServerMap"]));
            }
            set {
                this["DefaultServerMap"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string DataDir {
            get {
                return ((string)(this["DataDir"]));
            }
            set {
                this["DataDir"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Ark Server Manager")]
        public string DefaultDataDir {
            get {
                return ((string)(this["DefaultDataDir"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ShooterGameServer")]
        public string ServerProcessName {
            get {
                return ((string)(this["ServerProcessName"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("GameUserSettings.ini")]
        public string ServerGameUserSettingsFile {
            get {
                return ((string)(this["ServerGameUserSettingsFile"]));
            }
            set {
                this["ServerGameUserSettingsFile"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".ini")]
        public string IniExtension {
            get {
                return ((string)(this["IniExtension"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("*.profile,*.ini")]
        public string LoadProfileExtensionList {
            get {
                return ((string)(this["LoadProfileExtensionList"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MachinePublicIP {
            get {
                return ((string)(this["MachinePublicIP"]));
            }
            set {
                this["MachinePublicIP"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://whatismyip.akamai.com/")]
        public string PublicIPCheckUrl {
            get {
                return ((string)(this["PublicIPCheckUrl"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ManageFirewallAutomatically {
            get {
                return ((bool)(this["ManageFirewallAutomatically"]));
            }
            set {
                this["ManageFirewallAutomatically"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Logs")]
        public string LogsDir {
            get {
                return ((string)(this["LogsDir"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("?MultiHome={0}")]
        public string ServerCommandLineArgsIPMatchFormat {
            get {
                return ((string)(this["ServerCommandLineArgsIPMatchFormat"]));
            }
            set {
                this["ServerCommandLineArgsIPMatchFormat"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://api.ark.bar/version")]
        public string AvailableVersionUrl {
            get {
                return ((string)(this["AvailableVersionUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://arkdedicated.com/version")]
        public string AvailableVersionUrl2 {
            get {
                return ((string)(this["AvailableVersionUrl2"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://api.ark.bar/server/{0}/{1}")]
        public string ServerStatusUrlFormat {
            get {
                return ((string)(this["ServerStatusUrlFormat"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://arkservermanager.s3.amazonaws.com/release/latest.txt")]
        public string LatestASMVersionUrl {
            get {
                return ((string)(this["LatestASMVersionUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int UpdateCheckTime {
            get {
                return ((int)(this["UpdateCheckTime"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("97D490F696FA0B36EB7141B458040113")]
        public string SteamAPIKey {
            get {
                return ((string)(this["SteamAPIKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ShooterGame\\Saved\\SavedArks")]
        public string SavedArksRelativePath {
            get {
                return ((string)(this["SavedArksRelativePath"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SteamUserName {
            get {
                return ((string)(this["SteamUserName"]));
            }
            set {
                this["SteamUserName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SteamPassword {
            get {
                return ((string)(this["SteamPassword"]));
            }
            set {
                this["SteamPassword"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://arkservermanager.s3.amazonaws.com/release/latest.zip")]
        public string ASMDownloadUrl {
            get {
                return ((string)(this["ASMDownloadUrl"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UpgradeConfig {
            get {
                return ((bool)(this["UpgradeConfig"]));
            }
            set {
                this["UpgradeConfig"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ASMScheduler")]
        public string SchedulerWorkDir {
            get {
                return ((string)(this["SchedulerWorkDir"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ServerCacheDir {
            get {
                return ((string)(this["ServerCacheDir"]));
            }
            set {
                this["ServerCacheDir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int ServerCacheUpdatePeriod {
            get {
                return ((int)(this["ServerCacheUpdatePeriod"]));
            }
            set {
                this["ServerCacheUpdatePeriod"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int RCON_PlayerListSort {
            get {
                return ((int)(this["RCON_PlayerListSort"]));
            }
            set {
                this["RCON_PlayerListSort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int RCON_PlayerListFilter {
            get {
                return ((int)(this["RCON_PlayerListFilter"]));
            }
            set {
                this["RCON_PlayerListFilter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string RCON_AdminName {
            get {
                return ((string)(this["RCON_AdminName"]));
            }
            set {
                this["RCON_AdminName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool GLOBAL_EnableServerCache {
            get {
                return ((bool)(this["GLOBAL_EnableServerCache"]));
            }
            set {
                this["GLOBAL_EnableServerCache"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://arkservermanager.com")]
        public string HelpUrl {
            get {
                return ((string)(this["HelpUrl"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string OpenRCON_ServerIP {
            get {
                return ((string)(this["OpenRCON_ServerIP"]));
            }
            set {
                this["OpenRCON_ServerIP"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("32330")]
        public int OpenRCON_RCONPort {
            get {
                return ((int)(this["OpenRCON_RCONPort"]));
            }
            set {
                this["OpenRCON_RCONPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("en-US")]
        public string CultureName {
            get {
                return ((string)(this["CultureName"]));
            }
            set {
                this["CultureName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionAdministrationIsExpanded {
            get {
                return ((bool)(this["SectionAdministrationIsExpanded"]));
            }
            set {
                this["SectionAdministrationIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionAutomaticManagementIsExpanded {
            get {
                return ((bool)(this["SectionAutomaticManagementIsExpanded"]));
            }
            set {
                this["SectionAutomaticManagementIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionRulesIsExpanded {
            get {
                return ((bool)(this["SectionRulesIsExpanded"]));
            }
            set {
                this["SectionRulesIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionChatAndNotificationsIsExpanded {
            get {
                return ((bool)(this["SectionChatAndNotificationsIsExpanded"]));
            }
            set {
                this["SectionChatAndNotificationsIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionHUDAndVisualsIsExpanded {
            get {
                return ((bool)(this["SectionHUDAndVisualsIsExpanded"]));
            }
            set {
                this["SectionHUDAndVisualsIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionPlayerSettingsIsExpanded {
            get {
                return ((bool)(this["SectionPlayerSettingsIsExpanded"]));
            }
            set {
                this["SectionPlayerSettingsIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionDinoSettingsIsExpanded {
            get {
                return ((bool)(this["SectionDinoSettingsIsExpanded"]));
            }
            set {
                this["SectionDinoSettingsIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionEnvironmentIsExpanded {
            get {
                return ((bool)(this["SectionEnvironmentIsExpanded"]));
            }
            set {
                this["SectionEnvironmentIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionStructuresIsExpanded {
            get {
                return ((bool)(this["SectionStructuresIsExpanded"]));
            }
            set {
                this["SectionStructuresIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionEngramsIsExpanded {
            get {
                return ((bool)(this["SectionEngramsIsExpanded"]));
            }
            set {
                this["SectionEngramsIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionCustomLevelsIsExpanded {
            get {
                return ((bool)(this["SectionCustomLevelsIsExpanded"]));
            }
            set {
                this["SectionCustomLevelsIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SectionSOTFIsExpanded {
            get {
                return ((bool)(this["SectionSOTFIsExpanded"]));
            }
            set {
                this["SectionSOTFIsExpanded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public int CustomLevelXPIncrease_Player {
            get {
                return ((int)(this["CustomLevelXPIncrease_Player"]));
            }
            set {
                this["CustomLevelXPIncrease_Player"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public int CustomLevelXPIncrease_Dino {
            get {
                return ((int)(this["CustomLevelXPIncrease_Dino"]));
            }
            set {
                this["CustomLevelXPIncrease_Dino"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("version.txt")]
        public string VersionFile {
            get {
                return ((string)(this["VersionFile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("+login anonymous +force_install_dir \"{0}\"  \"+app_update 445400 {1}\" +quit")]
        public string SteamCmdInstallServerArgsFormat_SotF {
            get {
                return ((string)(this["SteamCmdInstallServerArgsFormat_SotF"]));
            }
        }
    }
}
