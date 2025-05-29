using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer.Model
{
    public class ControlStyler
    {
        public void ApplyRoundedCorners()
        {

        }
        public void ApplyRoundedCorners(Control control, int radius)
        {

            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90); // Esquina superior izquierda
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90); // Superior derecha
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90); // Inferior derecha
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90); // Inferior izquierda
            path.CloseFigure();

            control.Region = new Region(path); // Aplica el recorte redondeado

            control.Invalidate();
        }

    }
}
