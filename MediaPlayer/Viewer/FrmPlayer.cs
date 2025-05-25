using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaPlayer.Model;
using MediaPlayer.Presenter;
using MediaPlayer.Viewer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MediaPlayer
{
    public partial class FrmPlayer : Form, IPlayerView
    {
        // Implementación de la interfaz IPlayerView
        public event Action PlayPauseClicked;
        private readonly PlayerPresenter _presenter;
        public event Action StopClicked;
        public event Action forwardClicked;
        public event Action rewindClicked;

        public void UpdateProgressBar(int progress)
        {
            progressBar_playing.Value = progress;
        }

        public void UpdateButtonState(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Stopped:
                    btn_play.BackgroundImage = Properties.Resources.play1;
                    break;
                case PlayerState.Playing:
                    btn_play.BackgroundImage = Properties.Resources.pause;
                    break;
                case PlayerState.Paused:
                    btn_play.BackgroundImage = Properties.Resources.play1;
                    break;
            }
        }

        public void UpdateTimeDisplay(string currentTime, string totalTime)
        {
            // Actualiza el tiempo de reproducción y duración
            // lbl_currentTime.Text = _presenter.CurrentTime;
            // lbl_totalTime.Text = _presenter.TotalTime;
        }
        ControlStyler control_ = new ControlStyler();
        public FrmPlayer()
        {
            InitializeComponent();
            _presenter = new PlayerPresenter(this);
            control_.ApplyRoundedCorners(txt_secondary, 50);
            control_.ApplyRoundedCorners(txt_main, 50);
            progressBar_playing.Minimum = 0;
            progressBar_playing.Maximum = 1000;
        }


        private void btn_play_Click(object sender, EventArgs e)
        {
            PlayPauseClicked?.Invoke();
        }

        private void btn_forward_Click(object sender, EventArgs e)
        {
            forwardClicked?.Invoke();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            StopClicked?.Invoke();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            rewindClicked?.Invoke();
        }
    }
    }


