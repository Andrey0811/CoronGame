﻿using NAudio.Wave;

namespace CoronGame.SoundProvider
{
    public class LoopStream : WaveStream
    {
        private readonly WaveStream sourceStream;

        public LoopStream(WaveStream sourceStream, bool enableLooping)
        {
            this.sourceStream = sourceStream;
            EnableLooping = enableLooping;
        }

        private bool EnableLooping { get; }

        public override WaveFormat WaveFormat =>
            sourceStream.WaveFormat;

        public override long Length =>
            sourceStream.Length;

        public override long Position
        {
            get => sourceStream.Position;

            set => sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var totalBytesRead = 0;
            while (totalBytesRead < count)
            {
                var bytesRead = sourceStream.Read(buffer, 
                    offset + totalBytesRead, 
                    count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                        break;

                    sourceStream.Position = 0;
                }

                totalBytesRead += bytesRead;
            }

            return totalBytesRead;
        }
    }
}