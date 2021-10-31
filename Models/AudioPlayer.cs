using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models
{
    public class AudioPlayer
    {
        public void PlayAudio(byte[] audioData)
        {

            Stream stream = new MemoryStream(audioData);
            using (var player = new WaveOutEvent())
            using (var reader = new WaveFileReader(stream))
            {
                player.Init(reader);
                player.Play();
                while (player.PlaybackState != PlaybackState.Stopped)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
