﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCaddins.SheetCopier {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.6.0.0")]
    internal sealed partial class SheetCopierSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static SheetCopierSettings defaultInstance = ((SheetCopierSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SheetCopierSettings())));
        
        public static SheetCopierSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SC-Sheet_Category_Primary")]
        public string SheetParameterOne {
            get {
                return ((string)(this["SheetParameterOne"]));
            }
            set {
                this["SheetParameterOne"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SC-Sheet_Category_Secondary")]
        public string SheetParameterTwo {
            get {
                return ((string)(this["SheetParameterTwo"]));
            }
            set {
                this["SheetParameterTwo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CopyDetailingByDefault {
            get {
                return ((bool)(this["CopyDetailingByDefault"]));
            }
            set {
                this["CopyDetailingByDefault"] = value;
            }
        }
    }
}
