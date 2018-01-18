using System;
using System.Diagnostics;
using System.IO;

[assembly: InternalsVisibleTo("AssemblyB")]

namespace Logs
{
    public class Log
    {
        public const string LOG_NAME = "LogFrigobom";
        public const string SOURCE = "SyncFrigobom";

        public Log()
        {
            //verifica se o log já existe, se não existe então cria; 
            if (EventLog.SourceExists(SOURCE) == false)
                EventLog.CreateEventSource(SOURCE, LOG_NAME);
        }

        public void WriteEntry(string input, EventLogEntryType entryType)
        {
            //grava o texto na fonte de logs com o nome que      definimos para a constante SOURCE. 
            EventLog.WriteEntry(SOURCE, input, entryType);
        }

        public void WriteEntry(string input)
        {
            //loga um simples evento com a categoria de informação. 
            _WriteEntry(input, EventLogEntryType.Information);
        }
        public void WriteEntry(Exception ex)
        {
            //loga a ocorrência de uma excessão com a categoria de erro. 
            _WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }


    }
}
