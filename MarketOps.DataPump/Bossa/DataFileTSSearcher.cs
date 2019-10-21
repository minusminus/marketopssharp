using System;
using System.IO;
using System.Linq;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Sets stream on next line where ts is greater than specified.
    /// </summary>
    internal class DataFileTSSearcher
    {
        private readonly StreamReader _fileReader;

        public DataFileTSSearcher(StreamReader fileReader)
        {
            _fileReader = fileReader;
        }

        public bool Find(DateTime ts, out string prevLine)
        {
            ResetStreamToFirstDataLine();
            string searchHeader = PrepareSearchHeader(ReadLine(), ts);
            ResetStreamToFirstDataLine();
            return FindToEndOfFile(searchHeader, out prevLine);
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

        private bool FindToEndOfFile(string searchHeader, out string prevLine)
        {
            int linesRead = 0;
            string currLine = null;
            while (!_fileReader.EndOfStream)
            {
                prevLine = currLine;
                currLine = ReadLine();
                int x = LineGreaterThanTS(currLine, searchHeader);
                if (x == 0)
                {
                    prevLine = currLine;
                    return true;
                }
                if (x < 0)
                {
                    ResetStreamToDataLine(linesRead);
                    return true;
                }
                linesRead++;
            }
            prevLine = null;
            return false;
        }
    }
}
