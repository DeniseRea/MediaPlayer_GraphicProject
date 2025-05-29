using System.Drawing;
using System.Drawing.Drawing2D;

namespace MediaPlayer.Model
{
    public class StraightLine : Line
    {
        public StraightLine(Point start, Point end, Color color, float thickness = 1.0f)
            : base(start, end, color, thickness)
        {
        }

        public override void Draw(Graphics graphics)
        {
            if (!isVisible) return;

            using (Pen pen = new Pen(color, thickness))
            {
                graphics.DrawLine(pen, startPoint, endPoint);
            }
        }
    }
}