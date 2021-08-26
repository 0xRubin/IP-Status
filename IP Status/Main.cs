using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace IP_Status
{
    public partial class Main : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );
        Point lastPoint;
        public Main()
        {
            InitializeComponent();
            //Rounded Corners
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 18, 18));
        }
        //Exit Form
        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Minimize Window
        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Draggable Form
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ping ping = new Ping();
            try
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("NO IP entered", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    PingReply reply = ping.Send(textBox1.Text, 1000);

                    if (reply.Status == IPStatus.Success)
                    {
                        label3.ForeColor = Color.Green;
                        label3.Text = "Alive!";
                    }
                    else
                    {
                        label3.ForeColor = Color.Red;
                        label3.Text = "Downed!";
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("ArgumentNullException", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("NullReferenceException", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Exception", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var url = "https://discord.gg/xP6fw8h93P";
            var process = new System.Diagnostics.ProcessStartInfo();
            process.UseShellExecute = true;
            process.FileName = url;
            System.Diagnostics.Process.Start(process);
        }
    }
}