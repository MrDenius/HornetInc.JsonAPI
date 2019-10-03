using System.Collections.Generic;

namespace HornetInc.JsonAPI.Json
{
    public class Answer
    {
        public Answer(int id, List<Return> Returns)
        {
            this.id = id;
            this.Returns = Returns;
        }
        public int id;
        public List<Return> Returns;
    }
    public class Return
    {
        public Return(string Value)
        {
            this.Value = Value;
        }
        public string Value;
    }
}