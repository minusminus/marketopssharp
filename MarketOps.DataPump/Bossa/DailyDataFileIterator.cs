using System;
using System.IO;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa daily data file iterator.
    /// </summary>
    internal class DailyDataFileIterator : IDataFileIterator
    {
        private FileStream _file;
        private StreamReader _fileReader;

        private void SkipHeader()
        {
            _fileReader.ReadLine();
        }

        public void Open(string fileName)
        {
            _file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            _fileReader = new StreamReader(_file);
            SkipHeader();
        }

        public void Close()
        {
            _fileReader?.Dispose();
            _file?.Dispose();
        }

        public bool Eof()
        {
            return _fileReader.EndOfStream;
        }

        public string ReadLine()
        {
            return _fileReader.ReadLine();
        }

        public bool SetOnLineAfterTS(DateTime ts)
        {
            return (new DataFileTSSearcher(_fileReader)).Find(ts);
        }
    }
}
