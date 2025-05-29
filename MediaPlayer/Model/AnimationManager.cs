using System;
using System.Drawing;
using System.Windows.Forms;

namespace MediaPlayer.Model
{
    public enum AnimationType
    {
        None,
        Rotation,
        Scaling,
        RotationAndScaling,
        Pulse,
        Dynamic
    }

    public class AnimationManager
    {
        private Timer animationTimer;
        private DrawableShape target;
        private AnimationType currentAnimation;
        private float rotationAngle = 0f;
        private float scaleValue = 1f;
        private bool scalingUp = true;
        private float rotationSpeed = 2f;
        private float scaleSpeed = 0.02f;
        private float minScale = 0.5f;
        private float maxScale = 1.5f;

        public bool IsRunning => animationTimer?.Enabled ?? false;
        public AnimationType CurrentAnimation => currentAnimation;

        public AnimationManager(DrawableShape target, int fps = 20)
        {
            System.Diagnostics.Debug.WriteLine($"AnimationManager constructor - target is null: {target == null}, fps: {fps}");
            
            this.target = target;
            
            animationTimer = new Timer();
            animationTimer.Interval = 1000 / fps;
            animationTimer.Tick += UpdateAnimation;
            
            currentAnimation = AnimationType.None;
            
            System.Diagnostics.Debug.WriteLine($"AnimationManager created - Timer interval: {animationTimer.Interval}ms");
        }

        public void SetTarget(DrawableShape newTarget)
        {
            System.Diagnostics.Debug.WriteLine($"SetTarget called - newTarget is null: {newTarget == null}");
            target = newTarget;
        }

        public void StartAnimation(AnimationType type)
        {
            System.Diagnostics.Debug.WriteLine($"StartAnimation called with type: {type}");
            System.Diagnostics.Debug.WriteLine($"target is null: {target == null}");
            System.Diagnostics.Debug.WriteLine($"animationTimer is null: {animationTimer == null}");
            
            currentAnimation = type;
            if (type != AnimationType.None && target != null && animationTimer != null)
            {
                animationTimer.Start();
                System.Diagnostics.Debug.WriteLine($"Timer started. IsRunning: {animationTimer.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Current animation set to: {currentAnimation}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Animation NOT started - Type: {type}, Target null: {target == null}, Timer null: {animationTimer == null}");
            }
        }

        public void StopAnimation()
        {
            System.Diagnostics.Debug.WriteLine("StopAnimation called");
            animationTimer?.Stop();
            currentAnimation = AnimationType.None;

            // Resetear a estado original
            if (target != null)
            {
                target.ResetTransform();
                rotationAngle = 0f;
                scaleValue = 1f;
                scalingUp = true;
                System.Diagnostics.Debug.WriteLine("Animation reset to default values");
            }
        }

        public void PauseAnimation()
        {
            System.Diagnostics.Debug.WriteLine("PauseAnimation called");
            animationTimer?.Stop();
        }

        public void ResumeAnimation()
        {
            System.Diagnostics.Debug.WriteLine($"ResumeAnimation called - Current animation: {currentAnimation}");
            if (currentAnimation != AnimationType.None && target != null)
            {
                animationTimer?.Start();
                System.Diagnostics.Debug.WriteLine($"Animation resumed. IsRunning: {animationTimer?.Enabled}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Animation NOT resumed - Animation: {currentAnimation}, Target null: {target == null}");
            }
        }

        private void UpdateAnimation(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"UpdateAnimation called - Angle: {rotationAngle:F1}, Scale: {scaleValue:F2}, Animation: {currentAnimation}");

            if (target == null) 
            {
                System.Diagnostics.Debug.WriteLine("UpdateAnimation: target is null, returning");
                return;
            }

            try
            {
                target.ResetTransform();
                System.Diagnostics.Debug.WriteLine("Transform reset completed");

                switch (currentAnimation)
                {
                    case AnimationType.Rotation:
                        System.Diagnostics.Debug.WriteLine("Executing Rotation animation");
                        UpdateRotation();
                        break;

                    case AnimationType.Scaling:
                        System.Diagnostics.Debug.WriteLine("Executing Scaling animation");
                        UpdateScaling();
                        break;

                    case AnimationType.RotationAndScaling:
                        System.Diagnostics.Debug.WriteLine("Executing RotationAndScaling animation");
                        UpdateRotation();
                        UpdateScaling();
                        break;

                    case AnimationType.Pulse:
                        System.Diagnostics.Debug.WriteLine("Executing Pulse animation");
                        UpdatePulse();
                        break;

                    case AnimationType.Dynamic:
                        System.Diagnostics.Debug.WriteLine("Executing Dynamic animation");
                        UpdateDynamic();
                        break;
                        
                    default:
                        System.Diagnostics.Debug.WriteLine($"No animation handler for type: {currentAnimation}");
                        break;
                }

                // Aplicar transformaciones
                target.Rotate(rotationAngle);
                target.Scale(scaleValue, scaleValue);
                System.Diagnostics.Debug.WriteLine($"Transformations applied - Rotate: {rotationAngle:F1}, Scale: {scaleValue:F2}");

                // Evento para notificar que se necesita redibujar
                OnAnimationUpdated?.Invoke();
                System.Diagnostics.Debug.WriteLine("OnAnimationUpdated event fired");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateAnimation: {ex.Message}");
            }
        }

        private void UpdateRotation()
        {
            float oldAngle = rotationAngle;
            rotationAngle += rotationSpeed;
            if (rotationAngle >= 360f)
                rotationAngle = 0f;
            System.Diagnostics.Debug.WriteLine($"Rotation updated: {oldAngle:F1} -> {rotationAngle:F1} (speed: {rotationSpeed})");
        }

        private void UpdateScaling()
        {
            float oldScale = scaleValue;
            if (scalingUp)
            {
                scaleValue += scaleSpeed;
                if (scaleValue >= maxScale)
                {
                    scaleValue = maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                scaleValue -= scaleSpeed;
                if (scaleValue <= minScale)
                {
                    scaleValue = minScale;
                    scalingUp = true;
                }
            }
            System.Diagnostics.Debug.WriteLine($"Scale updated: {oldScale:F2} -> {scaleValue:F2} (direction: {(scalingUp ? "up" : "down")})");
        }

        private void UpdatePulse()
        {
            float time = Environment.TickCount * 0.005f;
            scaleValue = 0.8f + 0.4f * (float)Math.Sin(time);

            rotationAngle += 1f;
            if (rotationAngle >= 360f) rotationAngle = 0f;
            System.Diagnostics.Debug.WriteLine($"Pulse update - Angle: {rotationAngle:F1}, Scale: {scaleValue:F2}");
        }

        private void UpdateDynamic()
        {
            rotationSpeed += 0.05f;
            if (rotationSpeed > 8f) rotationSpeed = 1f;

            rotationAngle += rotationSpeed;
            if (rotationAngle >= 360f) rotationAngle = 0f;

            float time = Environment.TickCount * 0.003f;
            scaleValue = 0.6f + 0.8f * (float)Math.Abs(Math.Sin(time * 3));
            System.Diagnostics.Debug.WriteLine($"Dynamic update - Angle: {rotationAngle:F1}, Scale: {scaleValue:F2}, Speed: {rotationSpeed:F1}");
        }

        public void SetRotationSpeed(float speed) 
        {
            System.Diagnostics.Debug.WriteLine($"SetRotationSpeed: {rotationSpeed} -> {speed}");
            rotationSpeed = speed;
        }
        
        public void SetScaleSpeed(float speed) 
        {
            System.Diagnostics.Debug.WriteLine($"SetScaleSpeed: {scaleSpeed} -> {speed}");
            scaleSpeed = speed;
        }
        
        public void SetScaleRange(float min, float max)
        {
            System.Diagnostics.Debug.WriteLine($"SetScaleRange: [{minScale}, {maxScale}] -> [{min}, {max}]");
            minScale = min;
            maxScale = max;
        }

        public event Action OnAnimationUpdated;

        public void Dispose()
        {
            System.Diagnostics.Debug.WriteLine("AnimationManager disposing");
            animationTimer?.Stop();
            animationTimer?.Dispose();
        }
    }
}