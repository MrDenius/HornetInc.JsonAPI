using HornetInc.JsonAPI.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace HornetInc.JsonAPI
{
    public class JsonDocument : IDisposable
    {
        internal int id;
        WebClient wc = new WebClient();
        string UrlToApi;

        public JsonDocument(int id, string UrlToApi)
        {
            this.id = id;
            this.UrlToApi = UrlToApi;
        }

        public void NewCommand(string command)
        {
            Status status = JsonConvert.DeserializeObject<Status>(Read("RemoteJson"));

            if (status == null)
            {
                status = new Status(id, new List<Request>());
            }

            status.Requests.Add(new Request(command));
            Write(status, "RemoteJson");
        }

        public string[] ReadCommands()
        {
            Status status = JsonConvert.DeserializeObject<Status>(Read("RemoteJson"));

            if (status == null)
            {
                status = new Status(id, new List<Request>());
            }

            List<string> comms = new List<string>();

            if (status.Requests != null)
                foreach (Request req in status.Requests)
                {
                    if (req != null)
                        comms.Add(req.Command);
                }

            Write(new Status(id, new List<Request>()), "RemoteJson");

            return comms.ToArray();
        }

        public string[] GetAnswers()
        {
            Answer answer = JsonConvert.DeserializeObject<Answer>(Read("RemoteJsonAnswer"));

            if (answer == null)
            {
                answer = new Answer(id, new List<Return>());
            }

            List<string> comms = new List<string>();

            if (answer.Returns != null)
                foreach (Return ret in answer.Returns)
                {
                    if (ret != null)
                        comms.Add(ret.Value);
                }

            Write(new Answer(id, new List<Return>()), "RemoteJsonAnswer");

            return comms.ToArray();
        }

        public void NewAnswer(string value)
        {
            Answer answer = JsonConvert.DeserializeObject<Answer>(Read("RemoteJsonAnswer"));

            if (answer == null)
            {
                answer = new Answer(id, new List<Return>());
            }

            answer.Returns.Add(new Return(value));
            Write(answer, "RemoteJsonAnswer");
        }

        #region Премитивные команды
        private string Read(string ApiName)
        {
            return wc.DownloadString($"{UrlToApi}{ApiName}/{id}");
        }

        private void Write(string json, string ApiName)
        {
            wc.DownloadString($"{UrlToApi}{ApiName}/{id}?Json={json}");
        }

        private void Write(object json, string ApiName)
        {
            wc.DownloadString($"{UrlToApi}{ApiName}/{id}?Json={JsonConvert.SerializeObject(json)}");
        }

        public void Dispose()
        {
            wc.Dispose();
        }
        #endregion
    }
}