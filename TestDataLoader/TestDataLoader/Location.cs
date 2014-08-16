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
using System.Windows.Markup;

namespace NicholasArmstrong.Posts.TestDataLoader
{
    /// <summary>
    /// Represents a location in a smart home.
    /// </summary>
    [Serializable]
    [ContentProperty("Name")]
    public class Location
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location's numerical identifier.
        /// </summary>
        public int Id { get; set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Produces a text representation of the location.
        /// </summary>
        /// <returns>A text representation of the location.</returns>
        public override string ToString()
        {
            return this.Name;
        } 
        #endregion
    }
}
