//-----------------------------------------------------------------------
// XAML Test Data Loader Sample Application by Nicholas Armstrong
// Available online at http://nicholasarmstrong.com
//
// This code is designed for illustration purposes only.  
// Exception handling and other coding practices required for production
// systems may have been ignored in order to reduce this code to its 
// simplest possible form.
//-----------------------------------------------------------------------

using System.Collections.ObjectModel;

namespace NicholasArmstrong.Posts.TestDataLoader
{
    /// <summary>
    /// The TestDataLoader application view model.
    /// </summary>
    public class ViewModel
    {
        #region Fields
        /// <summary>
        /// The locations used in the application
        /// </summary>
        private ObservableCollection<Location> locations = new ObservableCollection<Location>();

        /// <summary>
        /// The devices used in the application.
        /// </summary>
        private ObservableCollection<Device> devices = new ObservableCollection<Device>(); 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the locations used in the application.
        /// </summary>
        public ObservableCollection<Location> Locations
        {
            get { return this.locations; }
        }

        /// <summary>
        /// Gets the devices used in the application.
        /// </summary>
        public ObservableCollection<Device> Devices
        {
            get { return this.devices; }
        } 
        #endregion
    }
}
