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
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Markup;
using System.Xml;

namespace NicholasArmstrong.Posts.TestDataLoader
{
    /// <summary>
    /// Class used to load test objects from a XAML file on disk; also, a nominal schema for a XAML test file.
    /// </summary>
    [Serializable]
    public class TestDataLoader
    {
        #region Fields
        /// <summary>
        /// The data loader locations list.
        /// </summary>
        private LocationList locations = new LocationList();

        /// <summary>
        /// The data loader devices list.
        /// </summary>
        private DeviceList devices = new DeviceList(); 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the locations in this data loader.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public LocationList Locations
        {
            get { return this.locations; }
        }

        /// <summary>
        /// Gets the devices in this data loader.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DeviceList Devices
        {
            get { return this.devices; }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Loads a XAML file from disk and returns an instance of this class.
        /// </summary>
        /// <param name="location">The location to read the XAML file from.</param>
        /// <returns>An instance of the TestLoader class, containing the objects to be loaded.</returns>
        public static TestDataLoader Load(string location)
        {
            return (TestDataLoader)XamlReader.Load(new XmlTextReader(location));
        }

        /// <summary>
        /// Saves the objects in this TestDataLoader to disk as a XAML file.
        /// </summary>
        /// <param name="location">The location to save the XAML file to.</param>
        public void Save(string location)
        {
            using (XmlTextWriter writer = new XmlTextWriter(location, Encoding.UTF8))
            {
                XamlWriter.Save(this, writer);
            }
        } 
        #endregion
    }

    #region Non-Generic List Classes
    /// <summary>
    /// Non-generic list class for locations.
    /// </summary>
    [Serializable]
    public class LocationList : List<Location> { }

    /// <summary>
    /// Non-generic list class for devices.
    /// </summary>
    [Serializable]
    public class DeviceList : List<Device> { }

    /// <summary>
    /// Non-generic list class for attributes.
    /// </summary>
    [Serializable]
    public class AttributeList : List<Attribute> { } 
    #endregion
}
