﻿using KoboErrorFinder.TablesExtensions.Executors;

namespace KoboErrorFinder.Services
{
    /// <summary>
    /// Сonduct the further execution of the program depending on the table type (depending on the MSF`s project). Calls sheet by sheet.
    /// </summary>
    public class ConductorService : IConductorService
    {
        private readonly Dictionary<string, IExecutor> executors = new Dictionary<string, IExecutor>();
        public void RegisterTable(string nameOfTable, IExecutor executor)
        {
            if (!executors.ContainsKey(nameOfTable))
            {
                executors.Add(nameOfTable, executor);
            }
            else
            {
                throw new Exception("ConductorService already has this executor");
            }
        }

        public IExecutor GetExecutor(string sheetName)
        {
            return executors[sheetName];
        }
    }
}
