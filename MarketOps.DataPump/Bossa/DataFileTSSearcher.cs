using System;
using System.IO;
using System.Linq;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Sets stream on next line with ts greater than specified.
    /// </summary>
    internal class DataFileTSSearcher
    {
        private readonly StreamReader _fileReader;

        public DataFileTSSearcher(StreamReader fileReader)
        {
            _fileReader = fileReader;
        }

        public bool Find(DateTime ts)
        {
            ResetStreamToFirstDataLine();
            string firstLine = ReadLine();
            string searchHeader = PrepareSearchHeader(firstLine, ts);
            int x = LineGreaterThanTS(firstLine, searchHeader);
            if (x < 0) return false;
            if (x == 0) return true;

            return FindToEndOfFile(searchHeader);
        }

        private void ResetStreamToBeginning()
        {
            _fileReader.DiscardBufferedData();
            _fileReader.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        private void ResetStreamToFirstDataLine()
        {
            ResetStreamToBeginning();
            _fileReader.ReadLine();
        }

        private void ResetStreamToDataLine(int linesToSkip)
        {
            ResetStreamToFirstDataLine();
            for (int i = 0; i < linesToSkip; i++)
                _fileReader.ReadLine();
        }

        private string ReadLine()
        {
            return _fileReader.ReadLine();
        }

        private string PrepareSearchHeader(string firstLine, DateTime ts)
        {
            return firstLine.Split(',')[0] + "," + ts.ToString("yyyyMMdd");
        }

        private int LineGreaterThanTS(string line, string search)
        {
            string current = String.Join(",", line.Split(',').Take(2));
            return String.CompareOrdinal(search, current);
        }

        private bool FindToEndOfFile(string searchHeader)
        {
            int linesRead = 1;
            while (!_fileReader.EndOfStream)
            {
                int x = LineGreaterThanTS(ReadLine(), searchHeader);
                if (x == 0) return true;
                if (x < 0)
                {
                    ResetStreamToDataLine(linesRead);
                    return true;
                }
                linesRead++;
            }
            return false;
        }
    }
}
