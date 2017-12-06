using System;
using System.Collections.Generic;

namespace PluginXforma59.Interface
{
    public interface IMachine
    {
        DateTime CurrentDateTime { get; }
        List<double> CPUThreadUtilizations { get; }
        List<double> GPUUtilizations { get; }
        List<double> FlowRates { get; }
        List<double> FlashersPower { get; }
        List<IDimmerControl> DimmerControls { get; }
    }
}
