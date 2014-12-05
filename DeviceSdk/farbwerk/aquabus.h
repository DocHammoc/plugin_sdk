#define UNIT_NONE             0
#define UNIT_RPM              1
#define UNIT_PERCENT          2
#define UNIT_CURRENT_MA       3
#define UNIT_CURRENT_A        4
#define UNIT_VOLTAGE_MV       5
#define UNIT_VOLTAGE_V        6
#define UNIT_SECONDS          7
#define UNIT_MILLIMETERS      8
#define UNIT_TEMPERATURE      9
#define UNIT_TEMPERATURE_ABS  10
#define UNIT_FLOW             11
#define UNIT_PRESSURE         12
#define FREQ_HZ               13
#define FREQ_KHZ              14
#define FREQ_MHZ              15
#define UNIT_WATT             16
#define UNIT_KWH              17
#define UNIT_KELVIN           18
#define UNIT_MS               19

typedef struct
{
   uint8_t type;		//bits:[0..3] = type, 2=output data, 1= sensor data
                        //bits:[4..7] = data id (sensor id0..3) or rgb output number
   uint16_t time;		//fade time in milliseconds or sensor data type
   int16_t data[3];		//output: data[0..3] range[0..8191] (R,G,B)
						//sensor: data[0]=sensor data   
   uint16_t crc;	    //crc, calculated from: type up to data[3]
}tFarberkReport;

//Samples
tFarberkReport sensor_set=
{
   0x01,             //start
   (0<<4)|(2),       //software sensor 0 and 2 for sensor
   UNIT_TEMPERATURE, //temperature
   {
     2500,	         //25°C
	 0,
	 0,
   },
	0,               //crc
};

tFarberkReport output_set=
{
   0x01,             //start
   (3<<4)|(1),       //rgb output 3, 1 for output data
   500,              //fade time 500ms to the new color values
   {
      8000,          //R, 80%
	  2000,          //G, 20%
	  10000,         //B, 100%
   },
   0,                //crc
};