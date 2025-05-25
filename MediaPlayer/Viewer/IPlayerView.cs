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
        
        //event Action ResumeClicked;
        /* event Action StopClicked;
         event Action NextClicked;
         event Action PreviousClicked;
         event Action returnClicked;
         event Action advanceClicked;*/
        
        void UpdateProgressBar(int progress);
        void UpdateButtonState(PlayerState state);
    }
}
