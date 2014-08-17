///-------------------------------------------------------------------------------
/// The WPF Title Animator (version 1.0)
/// By Nicholas Armstrong
/// August 2007
/// http://www.nicholasarmstrong.com/projects/
/// ------------------------------------------------------------------------------
/// This program is licensed under the Creative Commons Attribution-Share Alike
/// 2.5 Canada License.
/// http://creativecommons.org/licenses/by-sa/2.5/ca/deed.en_CA
/// 
/// Any conditions not covered by the above license must be obtained by contacting
/// the author through http://www.nicholasarmstrong.com.
/// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NicholasArmstrong.Projects.TitleAnimator
{
    class AnimatedTitle : INotifyPropertyChanged
    {
        #region Private Members
        private string _title;
        private ObservableCollection<string> _namesCollection = new ObservableCollection<string>();
        private AnimationType _animationType;
        #endregion

        #region Public Properties
        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged("Title"); }
        }

        public ObservableCollection<string> NamesCollection
        {
            get { return _namesCollection; }
            set { _namesCollection = value; NotifyPropertyChanged("NamesCollection"); }
        }

        public AnimationType AnimationType
        {
            get { return _animationType; }
            set { _animationType = value; NotifyPropertyChanged("AnimationType"); }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        } 
        #endregion

        #region Constructor
        public AnimatedTitle(AnimationType animationType, string title, params string[] names)
        {
            _animationType = animationType;
            Title = title;
            foreach (string name in names)
            {
                NamesCollection.Add(name);
            }
        } 
        #endregion
    }
}
