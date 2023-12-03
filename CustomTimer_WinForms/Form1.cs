using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomTimer_WinForms
{
    public partial class Timer : Form
    {
        private CustomTimer customTimer;

        public Timer()
        {
            InitializeComponent();
            customTimer = new CustomTimer(1000); // Interval set to 1000ms (1 second)
            customTimer.TimeElapsed += CustomTimer_TimeElapsed;
            customTimer.OnError += CustomTimer_OnError;
        }

        private void CustomTimer_TimeElapsed()
        {
            // Handle time elapsed event here
            Invoke((Action)delegate
            {
                // Update UI or perform actions
                label1.Text = DateTime.Now.ToString("HH:mm:ss");
            });
        }

        private void CustomTimer_OnError(Exception ex)
        {
            // Handle error event here
            MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            customTimer.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            customTimer.Stop();
        }
    }



}
