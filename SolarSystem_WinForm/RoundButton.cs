using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace lab7_planets
{
    public class RoundButton : Button
    {
        public RoundButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);

            this.Region = new Region(grPath);

            base.OnPaint(e);
        }
    }

}
