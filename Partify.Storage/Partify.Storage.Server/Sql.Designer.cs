﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Partify.Storage.Server {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Sql {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Sql() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Partify.Storage.Server.Sql", typeof(Sql).Assembly);
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
        ///   Looks up a localized string similar to SELECT Id, Name FROM Mode.
        /// </summary>
        internal static string AllModes {
            get {
                return ResourceManager.GetString("AllModes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Id, Name FROM Mode Where Id=@Id.
        /// </summary>
        internal static string ModeById {
            get {
                return ResourceManager.GetString("ModeById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Suggestion
        ///(Id, SpotifyIdFK, YoutubeIdFK, Count, Overruled, ModeIdFK) 
        ///VALUES (@Id, @SpotifyId, @YoutubeId, @Count, @Overruled, @ModeId).
        /// </summary>
        internal static string PostSuggestion {
            get {
                return ResourceManager.GetString("PostSuggestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO YoutubeVideo
        ///(Id, VideoId) 
        ///VALUES (@Id, @VideoId).
        /// </summary>
        internal static string PostVideo {
            get {
                return ResourceManager.GetString("PostVideo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO SpotifySong 
        ///(Id, SongId)
        ///VALUES 
        ///(@Id, @SongId).
        /// </summary>
        internal static string PostSong {
            get {
                return ResourceManager.GetString("PostSong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO UserSuggestion 
        ///(Id, SuggestionIdFK, UserIdFK)
        ///VALUES 
        ///(@Id, @SuggestionId, @UserId).
        /// </summary>
        internal static string PostUserSuggestion {
            get {
                return ResourceManager.GetString("PostUserSuggestion", resourceCulture);
            }
        }
    }
}
