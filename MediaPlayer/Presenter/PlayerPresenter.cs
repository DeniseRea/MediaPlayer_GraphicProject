using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaPlayer.Viewer;
using System.Windows.Forms;
using MediaPlayer.Model;

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
        MusicPlayer music = new MusicPlayer();

        public PlayerPresenter(IPlayerView view)
        {
            _view = view;
            _view.PlayPauseClicked += OnPlayPauseClicked;
            _view.StopClicked += StopPlayback; 
            _view.forwardClicked += ForwardPlayback; 

            _progressTimer = new Timer { Interval = 100 };
            _progressTimer.Tick += (s, e) => UpdateProgress();
            
            // Cargar música por defecto al inicializar
            LoadDefaultMusic();
            
            // Configurar eventos del MusicPlayer UNA VEZ
            ConfigureMusicPlayerEvents();
        }

        private void LoadDefaultMusic()
        {
            string defaultPath = "C:\\Users\\denise\\Documents\\GitHub\\MediaPlayer_GraphicProject\\MediaPlayer\\music\\AaronSmith_Chrono.mp3";
            
            if (music.LoadTrack(defaultPath))
            {
                // Asegurar que esté detenido después de cargar
                music.Stop();
                _currentState = PlayerState.Stopped;
                _view.UpdateButtonState(_currentState);
            }
            else
            {
                MessageBox.Show("Error al cargar la pista por defecto.");
            }
        }

        private void ConfigureMusicPlayerEvents()
        {
            music.PlaybackStarted += () => { 
                _currentState = PlayerState.Playing;
                _view.UpdateButtonState(_currentState);
            };
            
            music.PlaybackPaused += () => { 
                _currentState = PlayerState.Paused;
                _view.UpdateButtonState(_currentState);
            };
            
            music.PlaybackStopped += () => { 
                _currentState = PlayerState.Stopped;
                _view.UpdateButtonState(_currentState);
            };
            
            music.PlaybackEnded += () => { 
                _currentState = PlayerState.Stopped;
                _view.UpdateButtonState(_currentState);
            };
        }

        private void OnPlayPauseClicked()
        {
            switch (_currentState)
            {
                case PlayerState.Stopped:
                case PlayerState.Paused:
                    music.Play();
                    StartPlayback();
                    break;

                case PlayerState.Playing:
                    music.Pause();
                    PausePlayback();
                    break;
            }
            // NO cambies _currentState manualmente, los eventos del MusicPlayer lo harán
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

        private void StopPlayback()
        {
            _progressTimer.Stop();
            _currentProgress = 0;
            _currentState = PlayerState.Stopped;
            _view.UpdateButtonState(_currentState);
            music.Stop(); 
            // Aquí se detiene la animación
        }
        
        private void ForwardPlayback()
        {
            try
            {
                double currentPosition = music.GetCurrentPosition();
                double duration = music.GetDuration();
                
                if (duration <= 0) return; // No hay canción cargada
                
                double newPosition = currentPosition + 10; // Avanzar 10 segundos
                
                // Verificar que no se pase del final
                if (newPosition < duration)
                {
                    music.SetPosition(newPosition);
                }
                else
                {
                    // Si se pasa del final, ir casi al final
                    music.SetPosition(duration - 0.5);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ForwardPlayback: {ex.Message}");
            }
        }

        private void UpdateProgress()
        {
            try
            {
                double currentPosition = music.GetCurrentPosition();
                double duration = music.GetDuration();
                
                if (duration > 0)
                {
                    // Calcular progreso real basado en la duración de la canción
                    int progressPercentage = (int)((currentPosition / duration) * 1000);
                    _view.UpdateProgressBar(progressPercentage);
                    
                    // Si llegó al final, detener
                    if (currentPosition >= duration - 0.1)
                    {
                        StopPlayback();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateProgress: {ex.Message}");
            }
        }
    }


}