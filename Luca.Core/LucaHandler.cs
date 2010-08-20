﻿using System;
using System.IO;
using System.Linq;
using System.Web;
using Jint;

namespace Luca.Core
{
    public class LucaHandler : IHttpHandler
    {
        private LucaRequest _request;
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            _request = new LucaRequest(context.Request, new NameValueToJsonSerializer());
            var scriptContext = new ScriptContext(_request);
            var controllerPath = context.Server.MapPath("controllers") ?? "controllers";
            //var viewPath = context.Server.MapPath("views") ?? "views";
            loadJsFiles(scriptContext, controllerPath);
            var js = new JintRuntime(scriptContext);
            dynamic response = js.Execute();
            encode(response, context);
        }

        private void encode(dynamic response, HttpContext context)
        {
           //if (context.Request.ContentType.Contains("text/plain")) 
           //{
                context.Response.Write(response.ToString());
                return;
           //}
        }

        private void loadJsFiles(IScriptContext scriptContext, string pathToFiles)
        {
            Directory.GetFiles(pathToFiles).ToList()
                .ForEach(file => scriptContext.GetCurrentContext.AppendLine(
                    new StreamReader(file).ReadToEnd()
                    )
                );
        }
    }
}