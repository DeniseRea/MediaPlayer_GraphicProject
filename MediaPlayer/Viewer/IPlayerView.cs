using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaPlayer.Presenter;

namespace MediaPlayer.Viewer
{

    public interface IPlayerView
    {
        //listsners
        event Action PlayPauseClicked;
        event Action StopClicked;
        event Action forwardClicked;
        event Action rewindClicked;
        /* event Action StopClicked;
         event Action NextClicked;
         event Action PreviousClicked;
         event Action<int> VolumeChanged;*/

        void UpdateButtonState(PlayerState state);
        void UpdateProgressBar(int percentage);
       // void UpdateTrackName(string trackName);
        void UpdateTimeDisplay(string currentTime, string totalTime);
    }
}
