struct TaquastreamData{
	//1byte
	uchar transferId;							//ID muss zwingend 4 sein == Report ID

	//12byte
	uint rawSensors[6];						//alle Sensoren Im Unbearbeiteten Format
		#define RAW_SENSOR_FAN			0	//NTC Lüfter
		#define RAW_SENSOR_EXT			1	//NTC Externe Temperatur
		#define RAW_SENSOR_WATER		2	//NTC Intern Wassertemperatur
		#define RAW_FAN_VOLTAGE			3	//Spannung am Lüfterausgang
		#define RAW_12V_VOLTAGE			4	//Spannung 12V
		#define RAW_PUMP_CURRENT		5	//Pumpenstrom

	//6byte
	uint sensorData[3];						//temperaturen der 3 Temperatursensoren
		#define TEMP_SENSOR_FAN			0	//NTC Lüfter
		#define TEMP_SENSOR_EXT			1	//NTC Externe Temperatur
		#define TEMP_SENSOR_WATER		2	//NTC Intern Wassertemperatur

	//4byte
	uint frequency;							//ist die aktuelle Pumpenfrequenz
	uint maxFrequency;						//ist die automaitsch ermmitelte maximalfrequenz der Pumpe

	//9byte
	ulong flow;									//durchfluss (wenn konfiguriert== i2c aus)
	ulong fanRpm;								//ist die aktuelle drehzahl des Lüfters
	uchar fanPower;							//aktuelle Fan Ausgangsleistung

	//1byte
	uchar alarm;								//alarmauswertung der einzelnen Überwachungen
		#define ALARM_SENSOR1			0
		#define ALARM_SENSOR2			1
		#define ALARM_PUMP				2
		#define ALARM_FAN				3
		#define ALARM_FLOW				4
		#define ALARM_FAN_SHORT			5
		#define ALARM_FAN_TEMP90		6
		#define ALARM_FAN_TEMP70		7

	uchar mode;								//pumpenmodus und Reigeschaltete Keys
		#define MODE_PUMP_ADV			0	//Erweiterte Pumpeneinstellung
		#define MODE_FAN_AMP			1	//FAN Amp einstellungen
		#define MDOE_FAN_CONTROLLER		2	//Alle Features der Pumpe Benutzbar

	//16byte
	ulong controllerOut;						//reglerausgänge
	slong controllerI;
	slong controllerP;
	slong controllerD;

	//device info
	uint firmware;								//firmware version
	uint bootloader;							//OS version bzw. bootloader version
	uint hardwareVersion;					//Hardware
	uchar dummy1;								
	uchar dummy2;									
	uint serial;								//serial
	uchar publicKey[6];						//public key
};