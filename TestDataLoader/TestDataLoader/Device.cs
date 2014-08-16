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
using System.ComponentModel;
using System.Windows.Markup;

namespace NicholasArmstrong.Posts.TestDataLoader
{
    /// <summary>
    /// Represents a smart device used in the home.
    /// </summary>
    [Serializable]
    [ContentProperty("Attributes")]
    public class Device
    {
        #region Fields
        /// <summary>
        /// The attributes of this device that can be controlled.
        /// </summary>
        private AttributeList attributes = new AttributeList(); 
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the device's numerical identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the location where this device is located.
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets the attributes of this device that can be controlled.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AttributeList Attributes
        {
            get { return this.attributes; }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Produces a text representation of this device.
        /// </summary>
        /// <returns>A text representation of this device.</returns>
        public override string ToString()
        {
            return this.Name + ": " + this.attributes.Count + " attributes";
        } 
        #endregion
    }
}
