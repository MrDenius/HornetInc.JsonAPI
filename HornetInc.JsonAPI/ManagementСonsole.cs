using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HornetInc.JsonAPI
{
    public class ManagementСonsole : IDisposable
    {
        public JsonDocument JsonDoc;

        public delegate void DNewAnswers(string[] value);
        public event DNewAnswers NewAnswers;

        private Thread WaitAnsw;

        public ManagementСonsole(JsonDocument JsonDoc)
        {
            this.JsonDoc = JsonDoc;

            Initialization();
        }

        public void SendNewCommand(string command)
        {
            JsonDoc.NewCommand(command);
        }

        private void Initialization()
        {
            WaitAnsw = new Thread(WaitingAnswer);
            WaitAnsw.Start();
        }

        private void WaitingAnswer()
        {
            while (true)
            {
                List<string> values = new List<string>();
                foreach(string value in JsonDoc.GetAnswers())
                {
                    if (value != string.Empty && value != null)
                    {
                        values.Add(value);
                    }
                }
                if (values.Count != 0)
                    NewAnswers(values.ToArray());
                Thread.Sleep(250);
            }
        }

        public void Dispose()
        {
            WaitAnsw.Abort();
        }
    }
}
