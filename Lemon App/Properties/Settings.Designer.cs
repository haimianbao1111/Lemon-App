namespace Lemon_App.Properties {
    
    
    [System.Runtime.CompilerServices.CompilerGenerated()]
    internal sealed partial class Settings : System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [System.Configuration.UserScopedSettingAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Configuration.DefaultSettingValue("https://www.baidu.com/s?tn=mswin_oem_dg&ie=utf-16&word=%2a")]
        public string SearchUrl {
            get {
                return ((string)(this["SearchUrl"]));
            }
            set {
                this["SearchUrl"] = value;
            }
        }
        
        [System.Configuration.UserScopedSetting()]
        [System.Diagnostics.DebuggerNonUserCode()]
        [System.Configuration.DefaultSettingValue("LemonUser")]
        public string RobotName {
            get {
                return ((string)(this["RobotName"]));
            }
            set {
                this["RobotName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("北京")]
        public string WeatherInfo {
            get {
                return ((string)(this["WeatherInfo"]));
            }
            set {
                this["WeatherInfo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("个人签名")]
        public string Qianmin {
            get {
                return ((string)(this["Qianmin"]));
            }
            set {
                this["Qianmin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool RNBM {
            get {
                return ((bool)(this["RNBM"]));
            }
            set {
                this["RNBM"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsStart {
            get {
                return ((bool)(this["IsStart"]));
            }
            set {
                this["IsStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\"List\":[]}")]
        public string MusicList {
            get {
                return ((string)(this["MusicList"]));
            }
            set {
                this["MusicList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WebProxyUri {
            get {
                return ((string)(this["WebProxyUri"]));
            }
            set {
                this["WebProxyUri"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WebProxyUser {
            get {
                return ((string)(this["WebProxyUser"]));
            }
            set {
                this["WebProxyUser"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WebProxyPassWord {
            get {
                return ((string)(this["WebProxyPassWord"]));
            }
            set {
                this["WebProxyPassWord"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool isWebProxy {
            get {
                return ((bool)(this["isWebProxy"]));
            }
            set {
                this["isWebProxy"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0,0,0,0")]
        public global::System.Windows.Rect Hatop {
            get {
                return ((global::System.Windows.Rect)(this["Hatop"]));
            }
            set {
                this["Hatop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string UserImage {
            get {
                return ((string)(this["UserImage"]));
            }
            set {
                this["UserImage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LemonAreeunIts {
            get {
                return ((string)(this["LemonAreeunIts"]));
            }
            set {
                this["LemonAreeunIts"] = value;
            }
        }
    }
}
