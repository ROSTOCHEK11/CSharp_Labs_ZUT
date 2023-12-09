using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StopWatch_WinForms
{
    public partial class Form1 : Form
    {
        private CustomStopwatch stopwatch;

        public Form1()
        {
            InitializeComponent();
            stopwatch = new CustomStopwatch();
            stopwatch.TimeElapsed += UpdateTimeLabel;
            stopwatch.OnError += HandleError;
        }


        private void UpdateTimeLabel(TimeSpan time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<TimeSpan>(UpdateTimeLabel), time);
            }
            else
            {
                label1.Text = time.ToString(@"hh\:mm\:ss");
            }
        }

        private void HandleError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
        }




    }


}
