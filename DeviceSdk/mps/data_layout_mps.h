struct TDeviceData{
	uint16_t firmware;				//current firmware
	uint16_t bootloader;			//bootloader version
	uint16_t hardwareVersion;		//Hardware
	uint16_t serial;				//serial
	uint8_t publicKey[6];			//public key
};

struct Tdata{
	uint8_t transferId;					//ID is 3, USB Report ID
	uint16_t dataVersion;				//version
	struct TDeviceData devicedata;

	int16_t sensorData[7];					
		#define RAW_PRESSURE_AVG		0	//pressure
		#define RAW_SENSOR				1	//temperatur ext
		#define RAW_SENSOR_INT			2	//temperatur int
		#define RAW_PRESSURE_FILTERED	3
		#define RAW_PRESSURE_OFFSET		4
		#define RAW_PRESSUR_NORMALIZED	5
		#define RAW_PRESSURE			6

	uint8_t alarm_state;					//aktueller alarm status
	uint8_t pressureSensorType;		//drucksensor type

	int16_t sensor_none;		//#define PARM_NONE				0
	int16_t flow;				//#define PARM_FLOW				1
	int16_t pressure;			//#define PARM_PRESSURE			2
	int16_t level;				//#define PARM_LEVEL			3
	int16_t rpm;				//#define PARM_RPM				4
	int16_t tempeartures[2];	//#define PARM_TEMP				5 + 6
	int16_t pump;				//#define PARM_PUMP				7
};