//-----------------------------------------------------------------------
// XAML Test Data Loader Sample Application by Nicholas Armstrong
// Available online at http://nicholasarmstrong.com
//
// This code is designed for illustration purposes only.  
// Exception handling and other coding practices required for production
// systems may have been ignored in order to reduce this code to its 
// simplest possible form.
//-----------------------------------------------------------------------

using System;

namespace NicholasArmstrong.Posts.TestDataLoader
{
    /// <summary>
    /// Represents an attribute of a smart device that can be controlled.
    /// </summary>
    [Serializable]
    public class Attribute
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the attribute's numerical identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the power usage of the attribute.
        /// </summary>
        public string Power { get; set; }

        /// <summary>
        /// Gets or sets the display position of the attribute.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the type of attribute.
        /// </summary>
        public AttributeType Type { get; set; } 
        #endregion
    }
}
