using System;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Interface for data files iteration.
    /// 
    /// Iteration is forward only, to the end of file.
    /// </summary>
    internal interface IDataFileIterator
    {
        void Open(string fileName);
        void Close();
        bool Eof();
        string ReadLine();
        bool SetOnLineAfterTS(DateTime ts);
    }
}
