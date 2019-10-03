using System.Collections.Generic;

namespace HornetInc.JsonAPI.Json
{
    internal class Status
    {
        public Status(int id, List<Request> requests)
        {
            this.id = id;
            this.Requests = requests;
        }
        public int id;
        public List<Request> Requests;
    }
    internal class Request
    {
        public Request(string command)
        {
            this.Command = command;
        }
        public string Command;
    }
}