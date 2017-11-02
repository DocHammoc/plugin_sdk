using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AquaComputer.DeviceAccess.Interop;

namespace AquaComputer.DeviceAccess.Devices.Farbwerk
{
    /// <summary>
    /// native data mapping for farbwerk
    /// </summary>
    public class FarbwerkInterop
    {
        #region SoftwareSensorConfig
        /// <summary>
        /// software sensor configuration
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SoftwareSensorConfig
        {
            /// <summary>
            /// fallbacl value when no data is within the timeout received
            /// </summary>
            [MarshalAs(UnmanagedType.I2), EndianAttribute(Endianness.BigEndian)]
            public Int16 fallbck_value;

            /// <summary>
            /// timeout in milliseconds
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 timeout;

            /// <summary>
            /// if value != 0 the unit value from the sensor report is taken
            /// </summary>
            public byte auto_unit;

            /// <summary>
            /// unit type
            /// </summary>
            public byte unit;
        }
        #endregion

        #region HSVColor
        /// <summary>
        /// HSV Color
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HSVColor
        {
            /// <summary>
            /// Color: device Range [0..4095], has to scaled to 0..360°
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 h;

            /// <summary>
            /// saturation, range [0..4095] -> 0..100%
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 s;

            /// <summary>
            /// brightness, range [0..4095] -> 0..100%
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 v;
        }
        #endregion

        #region Controller
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Controller
        {
            /// <summary>
            /// controller mode:
            /// Sensor1 = 0,
            /// Sensor2 = 1,
            /// Sensor3 = 2,
            /// Sensor4 = 3,
            /// Softsensor1 = 4,
            /// Softsensor2 = 5,
            /// Softsensor3 = 6,
            /// Softsensor4 = 7,
            /// Manual = 8,
            /// External = 9,
            /// Auto1 = 10,
            /// Auto2 = 11,
            /// </summary>
            public byte mode;

            /// <summary>
            /// valid value from 0..100
            /// </summary>
            public byte effect_duration;

            /// <summary>
            /// controller speed: 0..100, fast:0, slow 100
            /// </summary>
            public byte time;

            /// <summary>
            /// input filter wight, filter the sensor value to prevent flickering colors
            /// </summary>
            public byte input_filter;

            /// <summary>
            /// 2 different setpoints for color fading
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2), EndianAttribute(Endianness.BigEndian)]
            public Int16[] setpoints;

            /// <summary>
            /// 2 diffent colors for fade
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public HSVColor[] colors;

            /// <summary>
            /// scale, 0..255 -> 0..100%, adjust the color balance between the diffent colors
            /// </summary>
            public byte scale_red;
            /// <summary>
            /// scale, 0..255 -> 0..100%, adjust the color balance between the diffent colors
            /// </summary>
            public byte scale_green;
            /// <summary>
            /// scale, 0..255 -> 0..100%, adjust the color balance between the diffent colors
            /// </summary>
            public byte scale_blue;

            /// <summary>
            /// effect curve 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] effect;

            /// <summary>
            /// direction of usage in the color circle, valid 0, 1
            /// </summary>
            public byte direction;

            /// <summary>
            /// filter the output to prevent color jumps
            /// </summary>
            public byte output_filter;

            /// <summary>
            /// rgb power on value for external mode
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] ext_rgb;
        }
        #endregion

        #region Settings
        /// <summary>
        /// Settings
        /// Report: id 3, feature report
        /// A crc16 as UInt16 at end of the report has to be added
        /// the report size is report id + [struct] + crc16
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Settings
        {            
            /// <summary>
            /// indicate the version of the structure
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 structure_id;

            /// <summary>
            /// aquabus address
            /// </summary>
            public byte i2c_address;

            /// <summary>
            /// bluetooth key, valif range 1000..9999
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 bt_key;

            /// <summary>
            /// mode of internal analog sensor
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4), EndianAttribute(Endianness.BigEndian)]
            public byte[] sensor_mode;

            /// <summary>
            /// temperature offset
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4), EndianAttribute(Endianness.BigEndian)]
            public Int16[] offset;

            /// <summary>
            /// 4 controller, one controller for each output
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Controller[] controller;

            /// <summary>
            /// software sensor configuration
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public SoftwareSensorConfig[] softsensor;           
        }
        #endregion

        #region DeviceInformations
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DeviceInformations
        {
            public byte report_id;

            /// <summary>
            /// structure version id
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 structure_id;

            /// <summary>
            /// device serial
            /// </summary>
            [MarshalAs(UnmanagedType.U4), EndianAttribute(Endianness.BigEndian)]
            public UInt32 serial;

            /// <summary>
            /// harware version
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 hardware;

            /// <summary>
            /// device type
            ///  FARBWERK_AQUABUS      0
            ///  FARBWERK_BLUETOOTH    1
            ///  FARBWERK_UART         2
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 device_type;

            /// <summary>
            /// bootloader version
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 bootloader;

            /// <summary>
            /// puplic device key
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] public_key;

            /// <summary>
            /// current firmware version
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 firmware;

            /// <summary>
            /// system state
            /// </summary>
            [MarshalAs(UnmanagedType.U4), EndianAttribute(Endianness.BigEndian)]
            public UInt32 system_state;
        }
        #endregion

        #region DeviceData
        /// <summary>
        /// current device data
        /// Report id: 1
        /// Report type: Input Report
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DeviceData
        {
            /// <summary>
            /// internal device time, is only a up counted value
            /// </summary>
            [MarshalAs(UnmanagedType.U4), EndianAttribute(Endianness.BigEndian)]
            public UInt32 time;

            /// <summary>
            /// adc raw values
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] adc;

            /// <summary>
            /// sensor units for all sensors
            /// 4 analog sensors + 4 software sensors
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8), EndianAttribute(Endianness.BigEndian)]
            public byte[] sensor_units;

            /// <summary>
            /// scaled sensor values
            /// 4 analog sensors + 4 software sensors
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8), EndianAttribute(Endianness.BigEndian)]
            public Int16[] sensor;

            /// <summary>
            /// current output mode for rgb oututs
            /// output 0 = output[0..2]
            /// output 1 = output[3..5]
            /// output 2 = output[6..8]
            /// output 3 = output[9..11]
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4), EndianAttribute(Endianness.BigEndian)]
            public byte[] output_mode;

            /// <summary>
            /// unscaled output values
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] outputs_unscaled;

            /// <summary>
            /// scaled output values
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] outputs;

            /// <summary>
            /// current led pwm output values
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] pwm;
        }
        #endregion

        #region SoftwareSensorReport
        /// <summary>
        /// Report id: 4, output report
        /// report to send 4 software sensor values to device
        /// A crc16 as UInt16 at end of the report has to be added
        /// the report size is report id + [struct] + crc16
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SoftwareSensorReport
        {
            /// <summary>
            /// sensor units
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4), EndianAttribute(Endianness.BigEndian)]
            public byte[] units;

            /// <summary>
            /// sensor data,
            /// 0x7fff indicate a not used sensor value
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4), EndianAttribute(Endianness.BigEndian)]
            public Int16[] data;
        }
        #endregion

        #region SoftwareSensorShortReport
        /// <summary>
        /// Report id: 7, output report
        /// shor sensor report, report only 1 sensor
        /// A crc16 as UInt16 at end of the report has to be added
        /// the report size is report id + [struct] + crc16
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SoftwareSensorShortReport
        {
            /// <summary>
            /// id of used sensor [0..3]
            /// </summary>
            public byte data_id;

            /// <summary>
            /// sensor type
            /// </summary>
            public byte units;

            /// <summary>
            /// sensor data
            /// </summary>
            [MarshalAs(UnmanagedType.I2), EndianAttribute(Endianness.BigEndian)]
            public Int16 data;
        }
        #endregion

        #region OverrideReport
        /// <summary>
        /// Report id: 5, output report
        /// send all output values to device
        /// A crc16 as UInt16 at end of the report has to be added
        /// the report size is report id + [struct] + crc16
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct OverrideReport
        {
            /// <summary>
            /// mitmask for output setup, each bit indicate a output
            /// bit[0..11] is used
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 mask;

            /// <summary>
            /// output value rgb1[0..3], ...
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] data;

            /// <summary>
            /// output fade time in milliseconds
            /// fade in this time to the new output value
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] time;
        }
        #endregion

        #region OverrideShortReport
        /// <summary>
        /// Report id: 6, output report
        /// set one output in the device
        /// A crc16 as UInt16 at end of the report has to be added
        /// the report size is report id + [struct] + crc16
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct OverrideShortReport
        {
            /// <summary>
            /// output id [0..11]
            /// </summary>
            public byte data_id;

            /// <summary>
            /// output value
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 data;

            /// <summary>
            /// fade time
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 time;
        }
        #endregion

        #region OverrideColorReport
        /// <summary>
        /// Report id: 8, output report
        /// send a color direct to the device
        /// A crc16 as UInt16 at end of the report has to be added
        /// the report size is report id + [struct] + crc16
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct OverrideColorReport
        {
            /// <summary>
            /// bitmask bit[0..11]
            /// setup the outputs that affected from this color]
            /// </summary>
            public byte mask;

            /// <summary>
            /// 4 fade times
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] time;

            /// <summary>
            /// setup the colors for each rgb output
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public HSVColor[] colors;
        }
        #endregion

        #region OverrideSettingsReport
        /// <summary>
        /// Report id: 9, output report
        /// write a setup for a color controller
        /// A crc16 as UInt16 at end of the report has to be added
        /// the report size is report id + [struct] + crc16
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct OverrideSettingsReport
        {
            /// <summary>
            /// controller id [0..3]
            /// </summary>
            public byte controller_id;

            /// <summary>
            /// new controller mode
            /// </summary>
            public byte mode;

            /// <summary>
            /// new colors
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public HSVColor[] colors;

            /// <summary>
            /// rgb power on value for external mode
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3), EndianAttribute(Endianness.BigEndian)]
            public UInt16[] ext_rgb;

            /// <summary>
            /// flags, 0x01, save date settings in device
            /// </summary>
            [MarshalAs(UnmanagedType.U2), EndianAttribute(Endianness.BigEndian)]
            public UInt16 time;
        }
        #endregion

    }
}
