﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NW.UnivariateForecasting.UnitTests.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NW.UnivariateForecasting.UnitTests.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to {
        ///    &quot;ObservationName&quot;: &quot;Sales USD&quot;,
        ///    &quot;Values&quot;: [
        ///        58.5,
        ///        615.26,
        ///        659.84,
        ///        635.69,
        ///        612.27,
        ///        632.94
        ///    ],
        ///    &quot;Coefficient&quot;: 0.5,
        ///    &quot;Error&quot;: 0.01
        ///}.
        /// </summary>
        internal static string ForecastingInitAsJson {
            get {
                return ResourceManager.GetString("ForecastingInitAsJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///    &quot;ObservationName&quot;: null,
        ///    &quot;Values&quot;: [
        ///        58.5,
        ///        615.26,
        ///        659.84,
        ///        635.69,
        ///        612.27,
        ///        632.94
        ///    ],
        ///    &quot;Coefficient&quot;: null,
        ///    &quot;Error&quot;: null
        ///}.
        /// </summary>
        internal static string ForecastingInitMinimalAsJson {
            get {
                return ResourceManager.GetString("ForecastingInitMinimalAsJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {&quot;Name&quot;:&quot;Dummy Observation&quot;,&quot;Interval&quot;:{&quot;Size&quot;:1,&quot;Unit&quot;:&quot;Months&quot;,&quot;StartDate&quot;:&quot;2020-07-01&quot;,&quot;EndDate&quot;:&quot;2020-08-01&quot;,&quot;TargetDate&quot;:&quot;2020-09-01&quot;,&quot;Steps&quot;:1,&quot;SubIntervals&quot;:1},&quot;X_Actual&quot;:632.94,&quot;C&quot;:0.82,&quot;E&quot;:0.22,&quot;Y_Forecasted&quot;:519.23,&quot;SlidingWindowId&quot;:&quot;Dummy Id&quot;}.
        /// </summary>
        internal static string ObservationWithDummyValues {
            get {
                return ResourceManager.GetString("ObservationWithDummyValues", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {&quot;Id&quot;:&quot;Dummy Id&quot;,&quot;ObservationName&quot;:&quot;Dummy Observation&quot;,&quot;Interval&quot;:{&quot;Size&quot;:6,&quot;Unit&quot;:&quot;Months&quot;,&quot;StartDate&quot;:&quot;2020-01-01&quot;,&quot;EndDate&quot;:&quot;2020-07-01&quot;,&quot;TargetDate&quot;:&quot;2020-08-01&quot;,&quot;Steps&quot;:1,&quot;SubIntervals&quot;:6},&quot;Items&quot;:[{&quot;Id&quot;:1,&quot;Interval&quot;:{&quot;Size&quot;:1,&quot;Unit&quot;:&quot;Months&quot;,&quot;StartDate&quot;:&quot;2020-01-01&quot;,&quot;EndDate&quot;:&quot;2020-02-01&quot;,&quot;TargetDate&quot;:&quot;2020-03-01&quot;,&quot;Steps&quot;:1,&quot;SubIntervals&quot;:1},&quot;X_Actual&quot;:58.5,&quot;Y_Forecasted&quot;:615.26},{&quot;Id&quot;:2,&quot;Interval&quot;:{&quot;Size&quot;:1,&quot;Unit&quot;:&quot;Months&quot;,&quot;StartDate&quot;:&quot;2020-02-01&quot;,&quot;EndDate&quot;:&quot;2020-03-01&quot;,&quot;TargetDate&quot;:&quot;2020-04-01&quot;,&quot;Ste [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SlidingWindowWithDummyValues {
            get {
                return ResourceManager.GetString("SlidingWindowWithDummyValues", resourceCulture);
            }
        }
    }
}
