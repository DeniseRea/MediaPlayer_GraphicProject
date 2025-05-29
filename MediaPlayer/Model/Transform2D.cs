using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MediaPlayer.Model
{
    public class Transform2D
    {
        private Matrix transformMatrix;

        public Transform2D()
        {
            transformMatrix = new Matrix();
        }

        // Traslación
        public void Translate(float dx, float dy)
        {
            transformMatrix.Translate(dx, dy);
        }

        // Escalamiento
        public void Scale(float scaleX, float scaleY)
        {
            transformMatrix.Scale(scaleX, scaleY);
        }

        // Rotación (ángulo en grados)
        public void Rotate(float angle)
        {
            transformMatrix.Rotate(angle);
        }

        // Rotación alrededor de un punto específico
        public void RotateAt(float angle, PointF center)
        {
            transformMatrix.RotateAt(angle, center);
        }

        // Aplicar transformación a un punto
        public PointF TransformPoint(PointF point)
        {
            PointF[] points = { point };
            transformMatrix.TransformPoints(points);
            return points[0];
        }

        // Aplicar transformación a múltiples puntos
        public PointF[] TransformPoints(PointF[] points)
        {
            PointF[] transformedPoints = new PointF[points.Length];
            Array.Copy(points, transformedPoints, points.Length);
            transformMatrix.TransformPoints(transformedPoints);
            return transformedPoints;
        }

        // Obtener la matriz de transformación para Graphics
        public Matrix GetMatrix()
        {
            return transformMatrix.Clone();
        }

        // Aplicar transformación directamente a Graphics
        public void ApplyToGraphics(Graphics graphics)
        {
            graphics.Transform = transformMatrix;
        }

        // Resetear transformaciones
        public void Reset()
        {
            transformMatrix.Reset();
        }

        // Combinar con otra transformación
        public void Multiply(Transform2D other)
        {
            transformMatrix.Multiply(other.transformMatrix);
        }

        // Liberar recursos
        public void Dispose()
        {
            transformMatrix?.Dispose();
        }
    }
}