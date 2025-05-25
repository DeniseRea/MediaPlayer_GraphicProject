using System;
using System.Collections.Generic;
using System.IO;
using WMPLib;

namespace MediaPlayer.Model
{
    public class MusicPlayer
    {
        private WindowsMediaPlayer _player;
        private List<string> _playlist;
        private int _currentTrackIndex;
        private bool _isInitialized;

        public event Action<double> PositionChanged;
        public event Action<string> TrackChanged;
        public event Action PlaybackStarted;
        public event Action PlaybackPaused;
        public event Action PlaybackStopped;
        public event Action PlaybackEnded;

        public MusicPlayer()
        {
            InitializePlayer();
            _playlist = new List<string>();
            _currentTrackIndex = 0;
        }

        private void InitializePlayer()
        {
            try
            {
                _player = new WindowsMediaPlayer();
                _player.PlayStateChange += OnPlayStateChange;
                _player.PositionChange += OnPositionChange;
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al inicializar Windows Media Player: {ex.Message}");
            }
        }

        // Cargar un archivo de música
        public bool LoadTrack(string filePath)
        {
            if (!_isInitialized || !File.Exists(filePath))
                return false;

            try
            {
                _player.URL = filePath;
                TrackChanged?.Invoke(Path.GetFileName(filePath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Agregar múltiples archivos a la playlist
        public void LoadPlaylist(string[] filePaths)
        {
            _playlist.Clear();
            foreach (string path in filePaths)
            {
                if (File.Exists(path) && IsAudioFile(path))
                {
                    _playlist.Add(path);
                }
            }

            if (_playlist.Count > 0)
            {
                _currentTrackIndex = 0;
                LoadTrack(_playlist[0]);
            }
        }

        // Reproducir
        public void Play()
        {
            if (_isInitialized && _player.URL != string.Empty)
            {
                _player.controls.play();
            }
        }

        // Pausar
        public void Pause()
        {
            if (_isInitialized)
            {
                _player.controls.pause();
            }
        }

        // Detener
        public void Stop()
        {
            if (_isInitialized)
            {
                _player.controls.stop();
            }
        }

        // Siguiente pista
        public bool NextTrack()
        {
            if (_playlist.Count == 0) return false;

            _currentTrackIndex = (_currentTrackIndex + 1) % _playlist.Count;
            return LoadTrack(_playlist[_currentTrackIndex]);
        }

        // Pista anterior
        public bool PreviousTrack()
        {
            if (_playlist.Count == 0) return false;

            _currentTrackIndex = _currentTrackIndex > 0 ? _currentTrackIndex - 1 : _playlist.Count - 1;
            return LoadTrack(_playlist[_currentTrackIndex]);
        }

        // Obtener posición actual
        public double GetCurrentPosition()
        {
            return _isInitialized ? _player.controls.currentPosition : 0;
        }

        // Obtener duración total
        public double GetDuration()
        {
            return _isInitialized && _player.currentMedia != null ? _player.currentMedia.duration : 0;
        }

        // Establecer posición
        public void SetPosition(double position)
        {
            if (_isInitialized)
            {
                _player.controls.currentPosition = position;
            }
        }

        // Obtener estado actual
        public WMPPlayState GetPlayState()
        {
            return _isInitialized ? _player.playState : WMPPlayState.wmppsUndefined;
        }

        // Establecer volumen (0-100)
        public void SetVolume(int volume)
        {
            if (_isInitialized)
            {
                _player.settings.volume = Math.Max(0, Math.Min(100, volume));
            }
        }

        // Eventos internos
        private void OnPlayStateChange(int newState)
        {
            switch ((WMPPlayState)newState)
            {
                case WMPPlayState.wmppsPlaying:
                    PlaybackStarted?.Invoke();
                    break;
                case WMPPlayState.wmppsPaused:
                    PlaybackPaused?.Invoke();
                    break;
                case WMPPlayState.wmppsStopped:
                    PlaybackStopped?.Invoke();
                    break;
                case WMPPlayState.wmppsMediaEnded:
                    PlaybackEnded?.Invoke();
                    break;
            }
        }

        private void OnPositionChange(double oldPosition, double newPosition)
        {
            PositionChanged?.Invoke(newPosition);
        }

        // Validar si es archivo de audio
        private bool IsAudioFile(string filePath)
        {
            string[] audioExtensions = { ".mp3", ".wav", ".wma", ".m4a", ".aac", ".flac", ".ogg" };
            string extension = Path.GetExtension(filePath).ToLower();
            return Array.Exists(audioExtensions, ext => ext == extension);
        }

        // Liberar recursos
        public void Dispose()
        {
            if (_player != null)
            {
                _player.close();
                _player = null;
            }
            _isInitialized = false;
        }
    }
}