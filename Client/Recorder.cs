using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Recorder : Form
    {
        Stopwatch stopwatch;
        WaveInEvent CaptureInstance;
        WaveFileWriter RecordedAudioWriter;
        public MemoryStream memoryStream;
        bool isDone = false;        
        public Recorder()
        {
            InitializeComponent();

            stopwatch = new Stopwatch();
            CaptureInstance = new WaveInEvent();
        }

        private void Recorder_Load(object sender, EventArgs e)
        {            
            stopwatch.Start();
            timer1.Start();

            //start record

            //Audio duoc luu lai trong Memory
            //Sau khi dispose RecordedAudioWriter thi memoryStream cung dispose theo -> phai dung IgnoreDisposeStream de record khong bi xoa
            memoryStream = new MemoryStream();
            RecordedAudioWriter = new WaveFileWriter(new IgnoreDisposeStream(memoryStream), CaptureInstance.WaveFormat);
            CaptureInstance.DataAvailable += (s, a) =>
            {
                RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
                if (RecordedAudioWriter.Position > RecordedAudioWriter.WaveFormat.AverageBytesPerSecond * 30)
                {
                    isDone = true;
                    CaptureInstance.StopRecording();
                }
            };

            // Bat buoc phai dispose RecordedAudioWriter thi audio moi nghe duoc
            CaptureInstance.RecordingStopped += RecordingStopped;

            CaptureInstance.StartRecording();
        }

        private void RecordingStopped(object s, StoppedEventArgs a)
        {
            RecordedAudioWriter.Dispose();
            RecordedAudioWriter = null;
            CaptureInstance.Dispose();
            CaptureInstance = null;
            this.Close();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Hien thi thoi gian ghi audio
            TimeSpan timeSpan = TimeSpan.FromSeconds(stopwatch.Elapsed.TotalSeconds);
            label1.Text = $"Recording: {timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            timer1.Stop();

            CaptureInstance.StopRecording();
            isDone = true;            
        }

        private void Recorder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Kiem tra xem nguoi dung bam done hay tat form
            if (isDone == true)
                DialogResult = DialogResult.OK;
            else
            {
                //Neu tat form thi phai kiem tra RecordedAudioWriter va xoa audio dang luu trong memory
                DialogResult = DialogResult.Cancel;

                if(RecordedAudioWriter != null || RecordedAudioWriter.CanWrite)
                {
                    CaptureInstance.RecordingStopped -= RecordingStopped;
                    CaptureInstance.StopRecording();
                    CaptureInstance.Dispose();
                    CaptureInstance = null;

                    RecordedAudioWriter.Dispose();
                    RecordedAudioWriter = null;

                    memoryStream = null;
                }
            }
                
        }
    }
}
