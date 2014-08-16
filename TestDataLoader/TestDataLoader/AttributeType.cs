//-----------------------------------------------------------------------
// XAML Test Data Loader Sample Application by Nicholas Armstrong
// Available online at http://nicholasarmstrong.com
//
// This code is designed for illustration purposes only.  
// Exception handling and other coding practices required for production
// systems may have been ignored in order to reduce this code to its 
// simplest possible form.
//-----------------------------------------------------------------------

namespace NicholasArmstrong.Posts.TestDataLoader
{
    /// <summary>
    /// Enumerates the types of attributes known to the system.
    /// </summary>
    public enum AttributeType
    {
        /// <summary>
        /// The attribute type is unknown.
        /// </summary>
        Other,

        /// <summary>
        /// The attribute is a lamp.
        /// </summary>
        Lamp,
        
        /// <summary>
        /// The attribute is a radio.
        /// </summary>
        Radio,

        /// <summary>
        /// The attribute is a fan.
        /// </summary>
        Fan
    }
}
