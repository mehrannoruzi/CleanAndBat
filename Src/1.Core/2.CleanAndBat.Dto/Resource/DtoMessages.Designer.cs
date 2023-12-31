﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanAndBat.Dto.Resource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DtoMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DtoMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CleanAndBat.Dto.Resource.DtoMessages", typeof(DtoMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to نام معتبر نمی باشد و باید بین 3 تا 25 کاراکتر داشته باشد..
        /// </summary>
        internal static string InvalidFirstName {
            get {
                return ResourceManager.GetString("InvalidFirstName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to نام خانوادگی معتبر نمی باشد و باید بین 5 تا 30 کاراکتر داشته باشد..
        /// </summary>
        internal static string InvalidLastName {
            get {
                return ResourceManager.GetString("InvalidLastName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to شماره موبایل معتبر نمی باشد..
        /// </summary>
        internal static string InvalidMobileNumber {
            get {
                return ResourceManager.GetString("InvalidMobileNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to طول پسوورد نباید کمتر از 8 کاراکتر باشد..
        /// </summary>
        internal static string InvalidPasswordLenght {
            get {
                return ResourceManager.GetString("InvalidPasswordLenght", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to شناسه کاربر معتبر نمی باشد..
        /// </summary>
        internal static string InvalidUserId {
            get {
                return ResourceManager.GetString("InvalidUserId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to نام کاربری یا کلمه عبور اشتباه می باشد..
        /// </summary>
        internal static string InvalidUsernameOrPassword {
            get {
                return ResourceManager.GetString("InvalidUsernameOrPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to طول پین کد ارسالی نباید کوچکتر از 4 کاراکتر باشد..
        /// </summary>
        internal static string PinCodeLenth {
            get {
                return ResourceManager.GetString("PinCodeLenth", resourceCulture);
            }
        }
    }
}
