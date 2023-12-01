using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab7_backgroundWorker
{
    public partial class Form1 : Form
    {
        private BackgroundWorker matrixWorker = new BackgroundWorker();
        private int[,] resultMatrix;

        public Form1()
        {
            InitializeComponent();
            matrixWorker.WorkerReportsProgress = true;
            matrixWorker.DoWork += MatrixWorker_DoWork;
            matrixWorker.ProgressChanged += MatrixWorker_ProgressChanged;
            matrixWorker.RunWorkerCompleted += MatrixWorker_RunWorkerCompleted;
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            int rowsA = int.Parse(textBoxRows1.Text);
            int colsA = int.Parse(textBoxCols1.Text);
            int rowsB = int.Parse(textBoxRows2.Text);
            int colsB = int.Parse(textBoxCols2.Text);

            // For simplicity, creating random matrices A and B with specified sizes
            int[,] matrixA = GenerateRandomMatrix(rowsA, colsA);
            int[,] matrixB = GenerateRandomMatrix(rowsB, colsB);

            progressBar.Minimum = 0;
            progressBar.Maximum = rowsA;

            matrixWorker.RunWorkerAsync(new Tuple<int[,], int[,]>(matrixA, matrixB));
        }

        private int[,] GenerateRandomMatrix(int rows, int cols)
        {
            Random rand = new Random();
            int[,] matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.Next(1, 10); // Filling with random integers (change range as needed)
                }
            }

            return matrix;
        }

        private void MatrixWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int[,] matrixA = ((Tuple<int[,], int[,]>)e.Argument).Item1;
            int[,] matrixB = ((Tuple<int[,], int[,]>)e.Argument).Item2;

            int rowsA = matrixA.GetLength(0);
            int colsB = matrixB.GetLength(1);

            resultMatrix = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    long sum = 0; // Use long for large matrices
                    for (int k = 0; k < matrixA.GetLength(1); k++)
                    {
                        sum += (long)matrixA[i, k] * matrixB[k, j]; // Use long for intermediate values
                    }
                    resultMatrix[i, j] = (int)sum;

                    // Report progress for each row multiplication
                    matrixWorker.ReportProgress(i + 1);
                }
            }
        }

        private void MatrixWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void MatrixWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Use the resulting matrix, display it, or perform any necessary action
            MessageBox.Show("Matrix multiplication completed!");
        }


    }
}
