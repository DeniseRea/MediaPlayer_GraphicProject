using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaPlayer.Viewer;
using System.Windows.Forms;

namespace MediaPlayer.Presenter

{
    public enum PlayerState
    {
        Stopped,  // Inicial/detenido
        Playing,  // Reproduciendo
        Paused    // En pausa
    }
    public class PlayerPresenter
    {
        private readonly IPlayerView _view;
        private readonly Timer _progressTimer;
        private const int TOTAL_DURATION = 60000; // 1 minuto en milisegundos
        private const int UPDATE_INTERVAL = 100;   // Actualizar cada 100ms (0.1 segundos)
        private PlayerState _currentState = PlayerState.Stopped;
        private int _currentProgress = 0;

        public PlayerPresenter(IPlayerView view)
        {
            _view = view;
            _view.PlayPauseClicked += OnPlayPauseClicked; // Un solo evento combinado
            _progressTimer = new Timer { Interval = 100 };
            _progressTimer.Tick += (s, e) => UpdateProgress();
        }

        private void OnPlayPauseClicked()
        {
            switch (_currentState)
            {
                case PlayerState.Stopped:
                    StartPlayback();
                    _currentState = PlayerState.Playing;
                    break;

                case PlayerState.Playing:
                    PausePlayback();
                    _currentState = PlayerState.Paused;
                    break;

                case PlayerState.Paused:
                    ResumePlayback(); // Lógica específica para reanudar
                    _currentState = PlayerState.Playing;
                    break;
            }
            _view.UpdateButtonState(_currentState); // Actualiza la UI

        }

        private void StartPlayback()
        {
            _progressTimer.Start();
            // se carga la animación
        }

        private void PausePlayback()
        {
            _progressTimer.Stop();
            // aqui e pausa igual la animacion 
        }

        private void ResumePlayback()
        {
            _progressTimer.Start();
            // continuar reproduciendo, sin iniciar otra vez
        }

        private void UpdateProgress()
        {
            _currentProgress += 100;
            if (_currentProgress >= 60000)
            {
                _currentProgress = 0;
                _progressTimer.Stop();
                _currentState = PlayerState.Stopped;
                _view.UpdateButtonState(_currentState);
            }
            _view.UpdateProgressBar((int)(_currentProgress / 60000.0 * 1000));
        }
    }


}
