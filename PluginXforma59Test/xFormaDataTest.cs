using System;
using NUnit.Framework;
using Shouldly;
using AquaComputer.Plugin;
using PluginXforma59;
using System.Collections.Generic;

namespace PluginXforma59Test
{
    [TestFixture]
    public class xFormaDataTest
    {
        private xFormaData _data;

        [OneTimeSetUp]
        public void RunOnce()
        {

        }

        [SetUp]
        public void RunBeforeEachTest()
        {
            _data = new xFormaData("test", Machine.CreateInstance(new List<MachineState>()
            {
                new MachineState() { CurrentDateTime = DateTime.Parse("11/12/2017 2:47:01"), CPUThreadUtilizations = new double[] { 10, 20, 30, 40 } },
                new MachineState() { CurrentDateTime = DateTime.Parse("11/12/2017 2:47:02"), CPUThreadUtilizations = new double[] { 15, 25, 35, 45 } },
                new MachineState() { CurrentDateTime = DateTime.Parse("11/12/2017 2:47:03"), CPUThreadUtilizations = new double[] { 12, 22, 32, 42 } },
            }),
            Configuration.Instance());
        }

        [Test]
        public void ProbeMachine_Basic_Success()
        {
            // Setup
            
            // Act
            SensorGroup group = _data.ProbeMachine();

            // Assert
            group.ShouldNotBeNull();
        }

        [Test]
        public void ProbeMachine_UtilizationFirstTime_NonZero()
        {
            // Setup

            // Act
            SensorGroup group = _data.ProbeMachine();
            SensorNode utilNode = group.GetSensor("test", "utilization");

            // Assert
            utilNode.sensor_value.ShouldBeGreaterThan(0);
        }

        [Test]
        public void ProbeMachine_UtilizationNextTime_NonZero()
        {
            // Setup

            // Act
            SensorGroup group1 = _data.ProbeMachine();
            SensorGroup group2 = _data.ProbeMachine();
            SensorNode utilNode = group2.GetSensor("test", "utilization");

            // Assert
            utilNode.sensor_value.ShouldBe(15);
        }

    }
}
