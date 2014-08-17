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
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Collections.ObjectModel;

namespace NicholasArmstrong.Projects.TitleAnimator
{
    public partial class Window1
    {
        #region Private Members
        private ObservableCollection<AnimatedTitle> _titles = new ObservableCollection<AnimatedTitle>();
        private int _currentTitle = 0;
        protected enum animationState { title1Completed, title2Completed, overlap1Completed, overlap2Completed, initalDelayCompleted, windowLoaded, disclaimerTextCompleted };
        
        #endregion

        #region Constructor
        public Window1()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
            _titles.Add(new AnimatedTitle(AnimationType.DistinctNoTitle, "", "The WPF Title Animator"));
            _titles.Add(new AnimatedTitle(AnimationType.DistinctNoTitle, "", "Created August 2007 by..."));
            _titles.Add(new AnimatedTitle(AnimationType.DistinctNoTitle, "", "Nicholas Armstrong"));
            _titles.Add(new AnimatedTitle(AnimationType.DistinctNoTitle, "", "Featuring titles that..."));
            _titles.Add(new AnimatedTitle(AnimationType.OverlapNoTitle, "", "Overlap..."));

            _titles.Add(new AnimatedTitle(AnimationType.Distinct, "Or are...", "Stacked"));
            _titles.Add(new AnimatedTitle(AnimationType.Overlap, "Or are both", "Stacked & Overlapped"));

            finalDisclaimer.Text = "Nicholas Armstrong, August 2007";
        } 
        #endregion

        #region Window Events
        private void Window_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.Close();
        }

        private void Window_SizeChanged(object sender, RoutedEventArgs e)
        {
            //Rescale the content to the window size:
            double scaleAmount = this.ActualWidth / this.Width;
            contentSize.ScaleX = scaleAmount;
            contentSize.ScaleY = scaleAmount;
        } 
        #endregion

        #region Title Animation
        
        //Load the next title into the correct title box
        private void DisplayNextTitle(animationState state)
        {
            Storyboard titleSet1AnimationStoryboard = (Storyboard)FindResource("titleSet1Animation");
            Storyboard titleSet2AnimationStoryboard = (Storyboard)FindResource("titleSet2Animation");
            Storyboard titleSet1OverlapAnimationStoryboard = (Storyboard)FindResource("titleSet1OverlapAnimation");
            Storyboard titleSet2OverlapAnimationStoryboard = (Storyboard)FindResource("titleSet2OverlapAnimation");
            Storyboard disclaimerTextAnimationStoryboard = (Storyboard)FindResource("disclaimerTextAnimation");
            Storyboard initialDelayAnimationStoryboard = (Storyboard)FindResource("initialDelayAnimation");
            Storyboard windowFadeOutAnimationStoryboard = (Storyboard)FindResource("windowFadeOutAnimation");
            Storyboard overlap1AnimationStoryboard = (Storyboard)FindResource("overlap1Animation");
            Storyboard overlap2AnimationStoryboard = (Storyboard)FindResource("overlap2Animation");

            if (_titles.Count > _currentTitle) //There are more titles to display
            {
                switch (state)
                {
                    case animationState.windowLoaded:
                        {
                            initialDelayAnimationStoryboard.Begin(this);
                            break;
                        }
                    case animationState.initalDelayCompleted:
                        {

                            nameBox1.ItemsSource = _titles[_currentTitle].NamesCollection;
                            if (_titles[_currentTitle].AnimationType == AnimationType.DistinctNoTitle || _titles[_currentTitle].AnimationType == AnimationType.OverlapNoTitle)
                            {
                                titleBox1.Visibility = Visibility.Collapsed;
                                titleSet1OverlapAnimationStoryboard.Begin(this);        //Use the overlap animation to accelerate the display of the name in a set without a title
                            }
                            else
                            {
                                titleBox1.Visibility = Visibility.Visible;
                                titleBox1.Text = _titles[_currentTitle].Title;
                                titleSet1AnimationStoryboard.Begin(this);
                            }
                            overlap1AnimationStoryboard.Begin(this);
                            _currentTitle++;
                            break;
                        }
                    case animationState.title1Completed:
                        {
                            if (_titles[_currentTitle].AnimationType != AnimationType.Overlap && _titles[_currentTitle].AnimationType != AnimationType.OverlapNoTitle)      //Overlapped Animations would already be running at this point
                            {
                                nameBox1.ItemsSource = _titles[_currentTitle].NamesCollection;
                                if (_titles[_currentTitle].AnimationType == AnimationType.DistinctNoTitle)
                                {
                                    titleBox1.Visibility = Visibility.Collapsed;
                                    titleSet1OverlapAnimationStoryboard.Begin(this);        //Use the overlap animation to accelerate the display of the name in a set without a title (not marked as such so it doesn't invalidate the above)
                                }
                                else
                                {
                                    titleBox1.Visibility = Visibility.Visible;
                                    titleBox1.Text = _titles[_currentTitle].Title;
                                    titleSet1AnimationStoryboard.Begin(this);
                                }
                                overlap1AnimationStoryboard.Begin(this);
                            }
                            _currentTitle++;
                            break;
                        }
                    case animationState.title2Completed:
                        {
                            if (_titles[_currentTitle].AnimationType != AnimationType.Overlap && _titles[_currentTitle].AnimationType != AnimationType.OverlapNoTitle)      //Overlapped Animations would already be running at this point
                            {
                                nameBox2.ItemsSource = _titles[_currentTitle].NamesCollection;
                                if (_titles[_currentTitle].AnimationType == AnimationType.DistinctNoTitle)
                                {
                                    titleBox2.Visibility = Visibility.Collapsed;
                                    titleSet2OverlapAnimationStoryboard.Begin(this);        //Use the overlap animation to accelerate the display of the name in a set without a title (not marked as such so it doesn't invalidate the above)
                                }
                                else
                                {
                                    titleBox2.Visibility = Visibility.Visible;
                                    titleBox2.Text = _titles[_currentTitle].Title;
                                    titleSet2AnimationStoryboard.Begin(this);
                                }
                                overlap2AnimationStoryboard.Begin(this);
                            }
                            _currentTitle++;
                            break;
                        }
                    case animationState.overlap1Completed:
                        {
                            //The overlap animation runs every time and signals when an overlapped title should begin.

                            if (_titles[_currentTitle].AnimationType == AnimationType.Overlap || _titles[_currentTitle].AnimationType == AnimationType.OverlapNoTitle)
                            {
                                //We need to overlap the next animation, start it now:
                                //Overlapping 1 with 2:
                                nameBox2.ItemsSource = _titles[_currentTitle].NamesCollection;
                                if (_titles[_currentTitle].AnimationType == AnimationType.OverlapNoTitle)
                                {
                                    titleBox2.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    titleBox2.Visibility = Visibility.Visible;
                                    titleBox2.Text = _titles[_currentTitle].Title;
                                }
                                titleSet2OverlapAnimationStoryboard.Begin(this);
                                overlap2AnimationStoryboard.Begin(this);
                            }
                            else //Don't overlap; the regular completed handlers will re-fill themselves then run again
                            {

                            }
                            break;
                        }
                    case animationState.overlap2Completed:
                        {
                            //The overlap animation runs every time and signals when an overlapped title should begin.

                            if (_titles[_currentTitle].AnimationType == AnimationType.Overlap || _titles[_currentTitle].AnimationType == AnimationType.OverlapNoTitle)
                            {
                                //We need to overlap the next animation, start it now:
                                //Overlapping 2 with 1:
                                nameBox1.ItemsSource = _titles[_currentTitle].NamesCollection;
                                if (_titles[_currentTitle].AnimationType == AnimationType.OverlapNoTitle)
                                {
                                    titleBox1.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    titleBox1.Visibility = Visibility.Visible;
                                    titleBox1.Text = _titles[_currentTitle].Title;
                                }
                                titleSet1OverlapAnimationStoryboard.Begin(this);
                                overlap1AnimationStoryboard.Begin(this);
                            }
                            else //Don't overlap; the regular completed handlers will re-fill themselves then run again
                            {

                            }
                            break;
                        }

                }
            }
            else if (state != animationState.overlap1Completed && state != animationState.overlap2Completed)    //No more titles; display the ending text
            {
                switch (state)
                {
                    case animationState.disclaimerTextCompleted:
                        {
                            windowFadeOutAnimationStoryboard.Begin(this);
                            break;
                        }
                    default:
                        {
                            disclaimerTextAnimationStoryboard.Begin(this);
                            break;
                        }
                }

            }
        }

        #region Animation Events
        void titleSet1Animation_Completed(object sender, EventArgs e)
        {
            DisplayNextTitle(animationState.title1Completed);
        }

        void titleSet2Animation_Completed(object sender, EventArgs e)
        {
            DisplayNextTitle(animationState.title2Completed);
        }

        void initialDelayAnimation_Completed(object sender, EventArgs e)
        {
            DisplayNextTitle(animationState.initalDelayCompleted);
        }

        void overlap1Animation_Completed(object sender, EventArgs e)
        {
            DisplayNextTitle(animationState.overlap1Completed);
        }

        void overlap2Animation_Completed(object sender, EventArgs e)
        {
            DisplayNextTitle(animationState.overlap2Completed);
        }

        void disclaimerTextAnimation_Completed(object sender, EventArgs e)
        {
            DisplayNextTitle(animationState.disclaimerTextCompleted);
        }

        void windowFadeOutAnimation_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

        void windowFadeInAnimation_Completed(object sender, EventArgs e)
        {
            DisplayNextTitle(animationState.windowLoaded);
        }  
        #endregion

        #endregion

    }
}