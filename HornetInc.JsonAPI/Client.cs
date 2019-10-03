using System;
using System.Collections.Generic;

namespace HornetInc.JsonAPI
{
    public class Client : IDisposable
    {
        List<JsonDocument> JDs = new List<JsonDocument>();
        List<ManagementСonsole> MCs = new List<ManagementСonsole>();
        public JsonDocument GetJson(int id)
        {
            foreach (JsonDocument JD in JDs)
            {
                if (JD.id == id)
                {
                    return JD;
                }
            }
            JDs.Add(new JsonDocument(id, "http://hornet.somee.com/api/"));
            return JDs[JDs.Count - 1];
        }

        public ManagementСonsole GetManagementСonsole(JsonDocument JsonDoc)
        {
            foreach (ManagementСonsole MCs in MCs)
            {
                if (MCs.JsonDoc.id == JsonDoc.id)
                {
                    return MCs;
                }
            }
            MCs.Add(new ManagementСonsole(JsonDoc));
            return MCs[JDs.Count - 1];
        }

        public JsonDocument GetJson(int id, string UrlToApi)
        {
            foreach (JsonDocument JD in JDs)
            {
                if (JD.id == id)
                {
                    return JD;
                }
            }
            JDs.Add(new JsonDocument(id, UrlToApi));
            return JDs[JDs.Count - 1];
        }

        public void Dispose()
        {
            foreach (JsonDocument JD in JDs)
            {
                JD.Dispose();
            }
            foreach (ManagementСonsole MC in MCs)
            {
                MC.Dispose();
            }
        }
    }
}
