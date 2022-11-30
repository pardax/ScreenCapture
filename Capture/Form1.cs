using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;

namespace Capture
{
    public partial class Form1 : Form
    {
        Point oriLocalPoint;
        Size oriLocalSize;

        bool original = true;
        bool isCaptured = false;

        Graphics screenG;
        Bitmap capWindow;

        System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'c')
            {
                original = false;
                isCaptured = true;

                this.Opacity = 0.0;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Location = new Point(0, 0);
                this.Size = Screen.PrimaryScreen.Bounds.Size;
                var fullScreen = Screen.PrimaryScreen.Bounds;

                capWindow = new Bitmap(fullScreen.Width, fullScreen.Height);

                screenG = Graphics.FromImage(capWindow);

                screenG.CopyFromScreen(PointToScreen(new Point(0, 0)), new Point(0, 0), fullScreen.Size);

                pbScreen.Image = capWindow;

                soundPlayer.SoundLocation = @"..\..\wav\capture.wav";
                soundPlayer.Play();

                this.Opacity = 1.0;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.Location = oriLocalPoint;
                this.Size = oriLocalSize;
                isCaptured = true;
            }
            else if(e.KeyChar == 'e')
            {
                soundPlayer.SoundLocation = @"..\..\wav\ereser.wav";
                soundPlayer.Play();

                isCaptured = false;
                pbScreen.Image = null;

                
            }
            else if(e.KeyChar == 's')
            {
                if (isCaptured)
                {
                    using (var Sfile = new SaveFileDialog())
                    {
                        Sfile.OverwritePrompt = true;
                        Sfile.FileName = "화면캡쳐";
                        Sfile.Filter = "이미지파일(*.jpg) | *.jpg";
                        DialogResult result = Sfile.ShowDialog();

                        if(result == DialogResult.OK)
                            capWindow.Save(Sfile.FileName, ImageFormat.Jpeg);       
                    }
                }
            }
            else
            {
                MessageBox.Show("캡쳐한 화면이 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (isCaptured)
            {
                oriLocalPoint = Location;
                oriLocalSize = Size;
            }
        }
    }
}
