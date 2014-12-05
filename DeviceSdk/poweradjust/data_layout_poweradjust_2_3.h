struct Tdata{
	uchar transferId;						//ID muss zwingend 3 sein == Report ID

	sint sensorData[4];						//
		#define TEMP_SENSOR_FAN			0	//NTC fan (temperature)
		#define TEMP_SENSOR_EXT			1	//NTC external Temperature
		#define FAN_CURRENT				2	//current fan
		#define FAN_VOLTAGE				3	//voltage fan

	uint flow;								//flow
	uint fanRpm;							//fan rpm
	uchar fanPower;							//fan power in 0..255

	uchar alarm;							//current alarm bits
		#define ALARM_SENSOR			0	//bit 0x01
		#define ALARM_FLOW				1	//bit 0x02
		#define ALARM_FAN_RPM			2	//bit 0x04
		#define ALARM_FAN_TEMP100		3	//bit 0x08
		#define ALARM_FAN_TEMP80		4	//bit 0x10

	uchar mode;								//device mode
		#define MODE_ADVANCED			0	//normal
		#define MODE_ULTRA				1	//Ultra

	uchar status;							//status register
		#define STARTBOOST_ACTIVE		0	//Startboost Aktiv

	ulong controllerOut[4];					//reglerausgang


	//device info
	uint firmware;				//firmware version
	uint bootloader;			//bootloader version
	uint hardwareVersion;		//Hardware
	uint serial;				//serial
	uchar publicKey[6];			//public key
	uchar dummy1;				
	uchar dummy2;				
};