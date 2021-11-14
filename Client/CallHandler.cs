using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class CallHandler: IDisposable
    {
        IPEndPoint IP;
        Socket Client;
        NetworkStream _networkStream;
        Thread recieveThread;
        Thread audioThread;
        WaveInEvent CaptureInstance;
        WaveFileWriter RecordedAudioWriter;
        public delegate void ConnectionFail();
        public ConnectionFail connectionFailHandler;
        public CallHandler()
        {                       
        }

        public void Start()
        {
            if (Connect())
            {
                audioThread = new Thread(Record);
                audioThread.IsBackground = true;
                audioThread.Start();

                recieveThread = new Thread(Receive);
                recieveThread.IsBackground = true;
                recieveThread.Start();
            }
        }

        bool Connect()//tạo kết nối
        {

            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9981);
            Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                Client.Connect(IP);
                _networkStream = new NetworkStream(Client);

                return true;
            }
            catch
            {
                if(connectionFailHandler != null)
                {
                    connectionFailHandler.Invoke();
                }
                return false;
            }
        }

        void Record()
        {
            CaptureInstance = new WaveInEvent();
            MemoryStream memoryStream = new MemoryStream();
            RecordedAudioWriter = new WaveFileWriter(new IgnoreDisposeStream(memoryStream), CaptureInstance.WaveFormat);

            var recordingFormat = WaveFormat.CreateIeeeFloatWaveFormat(48000, 2);
            CaptureInstance.WaveFormat = recordingFormat;
            CaptureInstance.DataAvailable += (s, a) =>
            {
                _networkStream.Write(a.Buffer, 0, a.BytesRecorded);
            };

            CaptureInstance.StartRecording();
        }

        void Receive()
        {
            try
            {
                byte[] data = new byte[1024 * 5000];
                int sampleRate = 16000; // 16 kHz
                int channels = 1; // mono
                int bits = 16;

                //var recordingFormat = new WaveFormat(sampleRate, bits, channels);
                var recordingFormat = WaveFormat.CreateIeeeFloatWaveFormat(48000, 2);
                var provider = new BufferedWaveProvider(recordingFormat);
                WaveOut o = new WaveOut();
                o.Init(provider);
                o.Volume = 1;
                o.Play();
                try
                {
                    while (true)
                    {
                        int recceived = _networkStream.Read(data, 0, Client.ReceiveBufferSize);
                        provider.AddSamples(data, 0, recceived);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch
            {
                //error
            }
        }

        public void Dispose()
        {
            if (CaptureInstance != null)
            {
                CaptureInstance.StopRecording();
                CaptureInstance.Dispose();
            }

            if (RecordedAudioWriter != null)
            {
                RecordedAudioWriter.Dispose();
            }

            if (audioThread != null && audioThread.IsAlive)
                audioThread.Abort();

            if (recieveThread != null && recieveThread.IsAlive)
                recieveThread.Abort();

            if (Client != null && Client.Connected)
            {
                Client.Close();
            }
        }
    }
}
