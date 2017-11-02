struct TDeviceData{
	uint16_t firmware;				//current firmware
	uint16_t bootloader;			//bootloader version
	uint16_t hardwareVersion;		//Hardware
	uint16_t serial;				//serial
	uint8_t publicKey[6];			//public key
};

#define P_SENSORTYPE_MP3V5004	0	//40mbar, 	400 mm
#define P_SENSORTYPE_MP3V5010	1	//100mbar, 	1000 mm
#define P_SENSORTYPE_MP3V5050	2	//500mbar, 	5000 mm
#define P_SENSORTYPE_MP3V5100	3	//1000mbar, 10000 mm
		
struct Tdata{
	uint8_t transferId;					//ID = 2, is USB Report ID
	uint16_t dataVersion;				//version
	struct TDeviceData devicedata;

	int16_t sensorData[7];
		#define RAW_PRESSURE_AVG		   0	//pressure
		#define RAW_SENSOR_EXT				1	//temperatur ext
		#define RAW_SENSOR_INT			   2	//temperatur int
		#define RAW_PRESSURE_FILTERED	   3
		#define RAW_PRESSURE_OFFSET		4
		#define RAW_PRESSUR_NORMALIZED	5
		#define RAW_PRESSURE			      6

	uint8_t alarm_state;					//current alarm state
	uint8_t pressureSensorType;		//pressure sensor type

	//data
	int16_t sensor_none;		   //#define PARM_NONE				0
	int16_t flow;				   //#define PARM_FLOW				1
	int16_t pressure;			   //#define PARM_PRESSURE			2
	int16_t level;				   //#define PARM_LEVEL			   3
	int16_t rpm;				   //#define PARM_RPM				4
	int16_t temperatures[2];	//#define PARM_TEMP			   5 + 6
	int16_t pump_power;        //#define PARM_PUMP				7

	uint8_t device_type;
      #define DEVICE_INVALID_0            0
      #define DEVICE_INVALID_1            1
      #define DEVICE_MPS_AQUALIS_LEVEL    2
      #define DEVICE_MPS_D5               3
      #define DEVICE_MPS_HIGHFLOW_USB     4
	   #define DEVICE_MPS_FLOW_100         5
      #define DEVICE_MPS_FLOW_200         6
      #define DEVICE_MPS_FLOW_400         7
      #define DEVICE_MPS_PRESSURE_40      8
      #define DEVICE_MPS_PRESSURE_100     9
      #define DEVICE_MPS_PRESSURE_500     10
      #define DEVICE_MPS_PRESSURE_1000    11

      #define DEVICE_FLAG_ALT1            0x40
      #define DEVICE_FLAG_ALT2            0x80
};