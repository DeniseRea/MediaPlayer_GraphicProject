using System.Drawing;

namespace MediaPlayer.Model
{
    public abstract class DrawableShape
    {
        protected Transform2D transform;
        protected Point position;
        protected Color color;

        public DrawableShape(Point position, Color color)
        {
            this.position = position;
            this.color = color;
            this.transform = new Transform2D();
        }

        // Método abstracto que cada figura debe implementar
        public abstract void DrawShape(Graphics graphics);

        // Método final que aplica transformaciones y dibuja
        public void Draw(Graphics graphics)
        {
            var originalTransform = graphics.Transform.Clone();
            
            try
            {
                // Aplicar transformaciones
                transform.ApplyToGraphics(graphics);
                
                // Dibujar la figura específica
                DrawShape(graphics);
            }
            finally
            {
                graphics.Transform = originalTransform;
            }
        }

        // Métodos de transformación disponibles para todas las figuras
        public virtual void Rotate(float angle)
        {
            transform.RotateAt(angle, new PointF(position.X, position.Y));
        }

        public virtual void Scale(float scaleX, float scaleY)
        {
            transform.Scale(scaleX, scaleY);
        }

        public virtual void Translate(float dx, float dy)
        {
            transform.Translate(dx, dy);
        }

        public virtual void ResetTransform()
        {
            transform.Reset();
        }

        public virtual void Dispose()
        {
            transform?.Dispose();
        }
    }
}