using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaPlayer.Viewer;
using System.Windows.Forms;
using MediaPlayer.Model;
using System.Drawing;

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
        private RadialCircle visualizer;
        // Añade esta variable para controlar actualizaciones de nivel de audio
        private Timer _audioLevelTimer;
        private Random _random = new Random();

        public PlayerPresenter(IPlayerView view)
        {
            _view = view;
            _view.PlayPauseClicked += OnPlayPauseClicked;
            _view.StopClicked += StopPlayback;
            _view.forwardClicked += ForwardPlayback;
            _view.rewindClicked += RewindPlayback;

            _progressTimer = new Timer { Interval = 100 };
            _progressTimer.Tick += (s, e) => UpdateProgress();

            // Cargar música por defecto al inicializar
            LoadDefaultMusic();

            // Configurar eventos del MusicPlayer UNA VEZ
            ConfigureMusicPlayerEvents();
            InitializeVisualizer();
        }

        private void LoadDefaultMusic()
        {
            string defaultPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Resources", "Neffex - Cold.mp3");

            if (music.LoadTrack(defaultPath))
            {
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

            // Iniciar animación en el visualizador
            UpdateVisualizer(0.5f); // Nivel base medio

            // Iniciar timer para simular cambios en el nivel de audio
            if (_audioLevelTimer == null)
            {
                _audioLevelTimer = new Timer { Interval = 50 };
                _audioLevelTimer.Tick += (s, e) => UpdateAudioLevels();
            }
            _audioLevelTimer.Start();
        }

        private void PausePlayback()
        {
            _progressTimer.Stop();
            _audioLevelTimer?.Stop(); // Detener actualizaciones de audio
        }

        private void ResumePlayback()
        {
            _progressTimer.Start();
            // continuar reproduciendo, sin iniciar otra vez
        }

        private void StopPlayback()
        {
            _progressTimer.Stop();
            _audioLevelTimer?.Stop(); // Detener actualizaciones de audio
            _currentProgress = 0;
            _currentState = PlayerState.Stopped;
            _view.UpdateButtonState(_currentState);
            music.Stop();

            // Actualizar visualizador a nivel mínimo
            UpdateVisualizer(0.1f);
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

        private void RewindPlayback()
        {
            try
            {
                double currentPosition = music.GetCurrentPosition();

                // Verificar que no se pase del inicio
                if (currentPosition > 10)
                {
                    music.SetPosition(currentPosition - 10); // Retroceder 10 segundos
                }
                else
                {
                    music.SetPosition(0); // Ir al inicio
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en RewindPlayback: {ex.Message}");
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

        private void InitializeVisualizer()
        {
            //Point center = _view.GetVisualizerCenter(pc_graph);
            //visualizer = new RadialCircle(center, 100); // Radio inicial de 100
        }

        public void UpdateVisualizer(float audioLevel)
        {
            // Convertir el nivel de audio a un radio entre 50 y 200
            int newRadius = 50 + (int)(audioLevel * 150);
            _view.UpdateVisualizer(newRadius);
        }

        public RadialCircle GetVisualizer()
        {
            return visualizer;
        }

        private void UpdateAudioLevels()
        {
            if (_currentState == PlayerState.Playing)
            {
                // Simular nivel de audio basado en la posición de reproducción
                // para crear un efecto visual más interesante
                double position = music.GetCurrentPosition();
                float baseLevel = 0.3f;
                float randomComponent = (float)_random.NextDouble() * 0.3f;
                float sinComponent = (float)Math.Sin(position * 0.5) * 0.4f;

                // Combinar componentes para un nivel entre 0.3 y 1.0
                float audioLevel = Math.Min(1.0f, baseLevel + randomComponent + Math.Abs(sinComponent));

                // Actualizar visualizador
                UpdateVisualizer(audioLevel);
            }
        }
    }


}