using System;
using System.Collections.Generic;
using System.Drawing;

namespace MediaPlayer.Model
{
    public class RadialCircle : DrawableShape  // Heredar de DrawableShape
    {
        private List<StraightLine> lines;
        private int radius;
        private int numberOfLines;

        // Constructor: llamar al constructor base
        public RadialCircle(Point center, int radius, int numberOfLines = 72) 
            : base(center, Color.White)  // Llamar al constructor padre
        {
            this.radius = radius;
            this.numberOfLines = numberOfLines;
            this.lines = new List<StraightLine>();
            GenerateLines();
        }

        // Implementar el método abstracto DrawShape
        public override void DrawShape(Graphics graphics)
        {
            foreach (var line in lines)
            {
                line.Draw(graphics);
            }
        }

        private void GenerateLines()
        {
            lines.Clear();
            double angleStep = 360.0 / numberOfLines;

            for (int i = 0; i < numberOfLines; i++)
            {
                double angle = i * angleStep;
                double radians = angle * Math.PI / 180.0;

                // Usar position en lugar de center
                Point endPoint = new Point(
                    (int)(position.X + radius * Math.Cos(radians)),
                    (int)(position.Y + radius * Math.Sin(radians))
                );

                // Usar color de la clase base
                StraightLine line = new StraightLine(position, endPoint, color, 1.0f);
                lines.Add(line);
            }
        }

        // Métodos específicos de RadialCircle
        public void SetRadius(int newRadius)
        {
            radius = newRadius;
            GenerateLines();
        }

        public void SetCenter(Point newCenter)
        {
            position = newCenter;  // Actualizar position de la clase base
            GenerateLines();
        }

        public void SetNumberOfLines(int newNumberOfLines)
        {
            numberOfLines = newNumberOfLines;
            GenerateLines();
        }

        public void SetColor(Color newColor)
        {
            color = newColor;  // Actualizar color de la clase base
            GenerateLines();
        }
    }
}