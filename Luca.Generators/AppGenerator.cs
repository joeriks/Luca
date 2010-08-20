﻿using System.IO;
using System.Collections.Generic;

namespace Luca.Generators
{
    public class AppGenerator : IGenerator
    {
        private readonly AppGeneratorParams _appGeneratorParams;

        public AppGenerator(AppGeneratorParams appGeneratorParams)
        {
            _appGeneratorParams = appGeneratorParams;
        }

        public void Generate(TextWriter output)
        {
            if (!IsNewFolder() && !IsFolderEmpty())
            {
                output.WriteLine("The folder needs to be empty to create a Luca application.");
                return;
            }
            CreateApplication();
        }

        private void CreateApplication()
        {
            if (!IsNewFolder()) CreateFolder(_appGeneratorParams.Path);
            var appStructure = GetApplicationStructure();
            foreach (var path in appStructure)
            {
                CreateFolder(_appGeneratorParams.Path + "\\" + path);
            }
        }

        private IEnumerable<string> GetApplicationStructure()
        {
            var list = new List<string>();
            list.Add("libs");
	        list.Add("tools");
	        list.Add("ui-web");
                list.Add("ui-web\\bin");
		        list.Add("ui-web\\controllers");
		        list.Add("ui-web\\views");
		        list.Add("ui-web\\view-models");
		        list.Add("ui-web\\config");
	        list.Add("app");
		        list.Add("app\\data");
		        list.Add("app\\services");
	        list.Add("specs");
		        list.Add("specs\\app");
			        list.Add("specs\\app\\data");
			        list.Add("specs\\app\\services");
		        list.Add("specs\\ui-web");
			        list.Add("specs\\ui-web\\controllers");
			        list.Add("specs\\ui-web\\config");
	        list.Add("behaviours");
		        list.Add("behaviours\\helpers");
	        list.Add("logs");
		        list.Add("logs\\app");
		        list.Add("logs\\specs");
		        list.Add("logs\\builds");
		        list.Add("logs\\behaviour");
		        list.Add("logs\\generators");
		        list.Add("logs\\migrations");
	        list.Add("reports");
		        list.Add("reports\\specs");
		        list.Add("reports\\behaviours");
	        list.Add("scripts");
		        list.Add("scripts\\builds");
		        list.Add("scripts\\generators");
		        list.Add("scripts\\migrations");		
	        list.Add("build");
            return list;
        }

        private void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }

        private bool IsNewFolder()
        {
            return !new DirectoryInfo(_appGeneratorParams.Path).Exists;
        }
        private bool IsFolderEmpty()
        {
            var dirinfo = new DirectoryInfo(_appGeneratorParams.Path);
            return (dirinfo.Exists && dirinfo.GetFiles().Length == 0 && dirinfo.GetDirectories().Length == 0);
        }

        public void Help(TextWriter output)
        {
            output.WriteLine("usage");
            output.WriteLine("Luca applicationName");
            output.WriteLine("Creates a folder by the applicationName and creates the structure inside");
            output.WriteLine("or");
            output.WriteLine("Luca .");
            output.WriteLine("Create the application structure in the folder where is been executed, the folder needs to be empty");
        }
    }
}