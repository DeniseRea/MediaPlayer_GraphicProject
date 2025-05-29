using System;
using System.Drawing;

namespace MediaPlayer.Model
{
    public abstract class Line
    {
        // Atributos básicos para cualquier línea
        protected Point startPoint;
        protected Point endPoint;
        protected Color color;
        protected float thickness;
        protected bool isVisible;
        
        // Propiedades públicas
        public Point StartPoint 
        { 
            get { return startPoint; } 
            set { startPoint = value; } 
        }
        
        public Point EndPoint 
        { 
            get { return endPoint; } 
            set { endPoint = value; } 
        }
        
        public Color Color 
        { 
            get { return color; } 
            set { color = value; } 
        }
        
        public float Thickness 
        { 
            get { return thickness; } 
            set { thickness = Math.Max(1, value); } // Mínimo grosor de 1
        }
        
        public bool IsVisible 
        { 
            get { return isVisible; } 
            set { isVisible = value; } 
        }
        
        // Constructor
        protected Line(Point start, Point end, Color lineColor, float lineThickness = 1.0f)
        {
            startPoint = start;
            endPoint = end;
            color = lineColor;
            thickness = lineThickness;
            isVisible = true;
        }
        
        // Métodos abstractos que deben implementar las clases hijas
        public abstract void Draw(Graphics graphics);
        
        // Métodos virtuales que pueden ser sobrescritos
        public virtual double GetLength()
        {
            return Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + 
                           Math.Pow(endPoint.Y - startPoint.Y, 2));
        }
        
        public virtual void Move(int deltaX, int deltaY)
        {
            startPoint = new Point(startPoint.X + deltaX, startPoint.Y + deltaY);
            endPoint = new Point(endPoint.X + deltaX, endPoint.Y + deltaY);
        }
        
        public virtual bool ContainsPoint(Point point, int tolerance = 5)
        {
            // Verificar si un punto está cerca de la línea
            double distance = DistanceFromPointToLine(point);
            return distance <= tolerance;
        }
        
        protected double DistanceFromPointToLine(Point point)
        {
            double A = endPoint.Y - startPoint.Y;
            double B = startPoint.X - endPoint.X;
            double C = endPoint.X * startPoint.Y - startPoint.X * endPoint.Y;
            
            return Math.Abs(A * point.X + B * point.Y + C) / Math.Sqrt(A * A + B * B);
        }
    }
}