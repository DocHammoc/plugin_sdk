using System;
using System.Collections.Generic;

namespace PluginXforma59.Interface
{
    public interface IMachine
    {
        DateTime CurrentDateTime { get; }
        int NumCPUProcessingThreads { get; }
        Dictionary<int, double> CPUThreadUtilization { get; }
    }
}
