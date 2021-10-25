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

        private string _previousLine, _currentLine;

        private void SkipHeader()
        {
            _fileReader.ReadLine();
        }

        private void InitLineVars()
        {
            _previousLine = null;
            _currentLine = null;
        }

        public void Open(string fileName)
        {
            _file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            _fileReader = new StreamReader(_file);
            SkipHeader();
            InitLineVars();
        }

        public void Close()
        {
            _fileReader?.Dispose();
            _file?.Dispose();
            InitLineVars();
        }

        public bool Eof()
        {
            return _fileReader.EndOfStream;
        }

        public string ReadLine()
        {
            _previousLine = _currentLine;
            _currentLine = _fileReader.ReadLine();
            return _currentLine;
        }

        public string PreviousLine()
        {
            return _previousLine;
        }

        public bool SetOnLineAfterTS(DateTime ts)
        {
            return (new DataFileTSSearcher(_fileReader)).Find(ts, out _currentLine);
        }
    }
}
