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
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace NicholasArmstrong.Posts.TestDataLoader
{
    /// <summary>
    /// The main window of the TestDataLoader application.
    /// </summary>
    public partial class Window1 : Window
    {
        #region Fields
        /// <summary>
        /// RoutedCommand to add a randomly-generated location to the data model.
        /// </summary>
        public static RoutedCommand AddLocationCommand = new RoutedCommand("AddLocation", typeof(Window1));

        /// <summary>
        /// RoutedCommand to add a randomly-generated device to the data model.
        /// </summary>
        public static RoutedCommand AddDeviceCommand = new RoutedCommand("AddDevice", typeof(Window1));

        /// <summary>
        /// RoutedCommand to save the data in the current data model to XAML.
        /// </summary>
        public static RoutedCommand SaveDataCommand = new RoutedCommand("SaveData", typeof(Window1));

        /// <summary>
        /// RoutedCommand to clear the data in the current data model.
        /// </summary>
        public static RoutedCommand ClearDataCommand = new RoutedCommand("ClearData", typeof(Window1));

        /// <summary>
        /// RoutedCommand to load a XAML file containing test data into the current data model.
        /// </summary>
        public static RoutedCommand LoadDataCommand = new RoutedCommand("LoadData", typeof(Window1));

        /// <summary>
        /// Random number used when generating devices and their attributes.
        /// </summary>
        private static Random rand = new Random(); 
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Window1 class, and binds the commands used in the application to their implementations.
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            this.CommandBindings.Add(new CommandBinding(AddLocationCommand, new ExecutedRoutedEventHandler(OnAddLocationCommand)));
            this.CommandBindings.Add(new CommandBinding(AddDeviceCommand, new ExecutedRoutedEventHandler(OnAddDeviceCommand)));
            this.CommandBindings.Add(new CommandBinding(SaveDataCommand, new ExecutedRoutedEventHandler(OnSaveDataCommand)));
            this.CommandBindings.Add(new CommandBinding(ClearDataCommand, new ExecutedRoutedEventHandler(OnClearDataCommand)));
            this.CommandBindings.Add(new CommandBinding(LoadDataCommand, new ExecutedRoutedEventHandler(OnLoadDataCommand)));
            this.InputBindings.Add(new KeyBinding(SaveDataCommand, Key.D, ModifierKeys.Control));
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Adds a randomly-generated location to the data model.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Arguments describing the event.</param>
        private static void OnAddLocationCommand(object sender, ExecutedRoutedEventArgs e)
        {
            int locationCount = ServiceProvider.ViewModel.Locations.Count;
            ServiceProvider.ViewModel.Locations.Add(new Location() { Id = locationCount, Name = "Location " + locationCount });
        }

        /// <summary>
        /// Adds a randomly-generated device to the data model.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Arguments describing the event.</param>
        private static void OnAddDeviceCommand(object sender, ExecutedRoutedEventArgs e)
        {
            int deviceCount = ServiceProvider.ViewModel.Devices.Count;
            Device newDevice = new Device() { Id = deviceCount, Name = "Device " + deviceCount };
            newDevice.LocationId = ServiceProvider.ViewModel.Locations.Count > 0 ? rand.Next(0, ServiceProvider.ViewModel.Locations.Count) : -1;

            int numAttributes = rand.Next(5);
            for (int i = 0; i < numAttributes; i++)
            {
                newDevice.Attributes.Add(new Attribute() { Id = i, Position = numAttributes - i, Name = "Attribute " + i, Type = (AttributeType)rand.Next(0, 3) });
            }

            ServiceProvider.ViewModel.Devices.Add(newDevice);
        }

        /// <summary>
        /// Saves the data in the data model to a XAML test data file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Arguments describing the event.</param>
        private static void OnSaveDataCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog() { AddExtension = true, ValidateNames = true };
            saveDialog.DefaultExt = ".xaml";
            saveDialog.Filter = "eXtensible Application Markup Language (XAML) files (*.xaml)|*.xaml|All files (*.*)|*.*";
            saveDialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (saveDialog.ShowDialog() == true)
            {
                TestDataLoader dataLoader = new TestDataLoader();
                dataLoader.Locations.AddRange(ServiceProvider.ViewModel.Locations);
                dataLoader.Devices.AddRange(ServiceProvider.ViewModel.Devices);
                dataLoader.Save(saveDialog.FileName);
                MessageBox.Show("Data model has been successfully saved to XAML file");
            }
        }

        /// <summary>
        /// Clears the data in the data model.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Arguments describing the event.</param>
        private static void OnClearDataCommand(object sender, ExecutedRoutedEventArgs e)
        {
            ServiceProvider.ViewModel.Locations.Clear();
            ServiceProvider.ViewModel.Devices.Clear();
        }

        /// <summary>
        /// Loads data into the data model from a XAML test file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Arguments describing the event.</param>
        private static void OnLoadDataCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.Filter = "eXtensible Application Markup Language (XAML) files (*.xaml)|*.xaml|All files (*.*)|*.*";
            openDialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (openDialog.ShowDialog() == true)
            {
                ServiceProvider.ViewModel.Locations.Clear();
                ServiceProvider.ViewModel.Devices.Clear();

                TestDataLoader dataLoader = TestDataLoader.Load(openDialog.FileName);

                foreach (var location in dataLoader.Locations)
                {
                    ServiceProvider.ViewModel.Locations.Add(location);
                }

                foreach (var device in dataLoader.Devices)
                {
                    ServiceProvider.ViewModel.Devices.Add(device);
                }

                MessageBox.Show("Data model has been successfully loaded from XAML file");
            }
        } 
        #endregion
    }
}
