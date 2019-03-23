using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Sixport;

namespace Sixport.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int z = 0; z < 32; z++)
            {
                int sample_length = 10;
                int sample_freq = 44100;
                int nuggets = sample_freq * sample_length / Constants.HEXTER_NUGGET_SIZE;
                double[] buffer = new double[sample_freq * sample_length];

                hexter_synth.ClearInstance();
                hexter_synth s = hexter_synth.Instance;

                s.instances.Add(new hexter_instance());
                s.instances[0].hexter_instance_select_program(z);
                s.instances[0].hexter_activate();

                int instanceCount = s.instances.Count;

                int lastoffset = 30;
                int offset = 30;

                // Sequencing
                List<snd_seq_event> events = new List<snd_seq_event>();
                for (int i = 0; i < nuggets; i++)
                {
                    events.Clear();
                    if (((i + 0) % 60) == 0)
                    {
                        events.Add(new snd_seq_event()
                        {
                            instance_index = 0,
                            note = (byte)(offset),
                            type = snd_seq_event_type.SND_SEQ_EVENT_NOTEON,
                            velocity = 110
                        });

                        lastoffset = offset;
                        offset += 6;
                    }
                    if (((i + 20) % 60) == 0)
                    {
                        events.Add(new snd_seq_event()
                        {
                            instance_index = 0,
                            note = (byte)(lastoffset),
                            type = snd_seq_event_type.SND_SEQ_EVENT_NOTEOFF,
                            velocity = 110
                        });
                    }
                    s.hexter_run_multiple_synths(Constants.HEXTER_NUGGET_SIZE, events);

                    for (int j = 0; j < Constants.HEXTER_NUGGET_SIZE; j++)
                    {
                        double res = 0.0;
                        for (int idx = 0; idx < instanceCount; idx++)
                        {
                            res += s.instances[idx].output[j];
                        }
                        buffer[(i * Constants.HEXTER_NUGGET_SIZE) + j] = res;
                    }
                }

                StreamWriter sw = new StreamWriter(@"..\..\Output\sample_" + z.ToString().PadLeft(3, '0') + ".wav", false);
                SaveIntoStream(sw.BaseStream, buffer, (long)nuggets * (long)Constants.HEXTER_NUGGET_SIZE);
                sw.Close();
                sw.Dispose();
            }
        }

        public static void SaveIntoStream(System.IO.Stream stream, double[] sampleData, long sampleCount)
        {
            // Export
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
            int RIFF = 0x46464952;
            int WAVE = 0x45564157;
            int formatChunkSize = 16;
            int headerSize = 8;
            int format = 0x20746D66;
            short formatType = 1;
            short tracks = 2;
            int samplesPerSecond = 44100;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int data = 0x61746164;
            int samples = (int)sampleCount;
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            writer.Write(RIFF);
            writer.Write(fileSize);
            writer.Write(WAVE);
            writer.Write(format);
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(samplesPerSecond);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(data);
            writer.Write(dataChunkSize);

            double sample_l;
            short sl;
            for (int i = 0; i < sampleCount; i++)
            {
                sample_l = sampleData[i] * 30000.0;
                if (sample_l < -32767.0f) { sample_l = -32767.0f; }
                if (sample_l > 32767.0f) { sample_l = 32767.0f; }
                sl = (short)sample_l;
                stream.WriteByte((byte)(sl & 0xff));
                stream.WriteByte((byte)(sl >> 8));
                stream.WriteByte((byte)(sl & 0xff));
                stream.WriteByte((byte)(sl >> 8));
            }
        }
    }
}
