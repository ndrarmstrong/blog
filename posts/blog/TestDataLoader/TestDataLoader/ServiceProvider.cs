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
    /// The root of the data model for the TestDataLoader application.
    /// </summary>
    public class ServiceProvider
    {
        #region Fields
        /// <summary>
        /// The instance of the ServiceProvider class to which all static calls are delegated.
        /// </summary>
        private static ServiceProvider instance;

        /// <summary>
        /// The application view model.
        /// </summary>
        private ViewModel viewModel; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the application ViewModel.
        /// </summary>
        public static ViewModel ViewModel
        {
            get
            {
                if (Instance.viewModel == null)
                {
                    Instance.viewModel = new ViewModel();
                }

                return Instance.viewModel;
            }
        }

        /// <summary>
        /// Gets the active instance of the ServiceProvider class.
        /// </summary>
        protected static ServiceProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceProvider();
                }

                return instance;
            }
        } 
        #endregion
    }
}
