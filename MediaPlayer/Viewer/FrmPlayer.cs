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
        ControlStyler control_ = new ControlStyler();
        public DrawableShape visualizer;
        private AnimationManager animationManager;


        public FrmPlayer()
        {
            InitializeComponent();
            _presenter = new PlayerPresenter(this);
            control_.ApplyRoundedCorners(txt_secondary, 50);
            control_.ApplyRoundedCorners(txt_main, 50);
            progressBar_playing.Minimum = 0;
            progressBar_playing.Maximum = 1000;

            Point center = GetVisualizerCenter(pc_graph);
            visualizer = new RadialCircle(center, 150);

            // Configurar AnimationManager
            SetupAnimationManager();

            pc_graph.Paint += Pc_graph_Paint;
        }

        private AnimationManager AnimationManager
        {
            get
            {
                if (animationManager == null)
                {
                    SetupAnimationManager();
                }
                return animationManager;
            }
        }

        private void SetupAnimationManager()
        {
            if (animationManager != null) return;

            if (visualizer == null)
            {
                Point center = GetVisualizerCenter(pc_graph);
                visualizer = new RadialCircle(center, 150);
            }

            animationManager = new AnimationManager(visualizer, 20);
            
            // Configurar parámetros
            animationManager.SetRotationSpeed(2f);
            animationManager.SetScaleSpeed(0.02f);
            animationManager.SetScaleRange(0.5f, 1.5f);
            
            // Suscribirse al evento
            animationManager.OnAnimationUpdated += () => pc_graph?.Invalidate();
            
            // FORZAR INICIO para probar que funciona
            animationManager.StartAnimation(AnimationType.RotationAndScaling);
            System.Diagnostics.Debug.WriteLine("Animation forced to start in constructor");
        }

        public void UpdateButtonState(PlayerState state)
        {
            System.Diagnostics.Debug.WriteLine($"UpdateButtonState called with state: {state}");

            switch (state)
            {
                case PlayerState.Stopped:
                    btn_play.BackgroundImage = Properties.Resources.play1;
                    AnimationManager.StopAnimation();
                    System.Diagnostics.Debug.WriteLine("Llamada a StopAnimation");
                    break;

                case PlayerState.Playing:
                    btn_play.BackgroundImage = Properties.Resources.pause;
                    AnimationManager.StartAnimation(AnimationType.RotationAndScaling);
                    System.Diagnostics.Debug.WriteLine("Llamada a StartAnimation con RotationAndScaling");
                    break;

                case PlayerState.Paused:
                    btn_play.BackgroundImage = Properties.Resources.play1;
                    AnimationManager.PauseAnimation();
                    System.Diagnostics.Debug.WriteLine("Llamada a PauseAnimation");
                    break;
            }
        }

        private void Pc_graph_Paint(object sender, PaintEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Pc_graph_Paint called");
            if (visualizer != null)
            {
                System.Diagnostics.Debug.WriteLine("Drawing visualizer");
                visualizer.Draw(e.Graphics);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Visualizer is null, not drawing");
            }
        }

        public void UpdateVisualizer(int radius)
        {
            if (visualizer is RadialCircle radialCircle)
            {
                Point center = GetVisualizerCenter(pc_graph);
                radialCircle.SetCenter(center);
                radialCircle.SetRadius(radius);
            }
            else
            {
                Point center = GetVisualizerCenter(pc_graph);
                visualizer = new RadialCircle(center, radius);
                animationManager.SetTarget(visualizer); // Actualizar target
            }
            pc_graph.Invalidate();
        }

        // Métodos para cambiar tipos de animación (puedes conectarlos a botones)
        public void SetAnimationType(AnimationType type)
        {
            animationManager.StartAnimation(type);
        }

        public void UpdateProgressBar(int progress)
        {
            progressBar_playing.Value = progress;
        }

        // Resto de métodos existentes...
        private void btn_play_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Play button clicked!");
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

        public Point GetVisualizerCenter(PictureBox pic)
        {
            if (pic == null)
            {
                throw new ArgumentNullException(nameof(pic), "El PictureBox no puede ser nulo");
            }

            int centerX = pic.Width / 2;
            int centerY = pic.Height / 2;
            return new Point(centerX, centerY);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            animationManager?.Dispose();
            base.OnFormClosed(e);
        }

        private void FrmPlayer_Load(object sender, EventArgs e)
        {

        }
    }
}

