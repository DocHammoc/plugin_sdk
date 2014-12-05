//*****************************************************************************
//* Name:
//*   DeviceStatus
//* Description:
//*   device state
//*****************************************************************************
typedef union {
   uint8_t data;
   struct {
      uint8_t power5V :1;    //5V available
      uint8_t power12V :1;   //12V available
   } Bits;
} DeviceStatus;

//*****************************************************************************
//* Name:
//*   SystemLockControl
//* Description:
//*   internal lock handling
//*****************************************************************************
typedef union {
   uint16_t data;
} SystemLockControl;

//*****************************************************************************
//* Name:
//*   DeviceType_t
//* Description:
//*   current device type
//*****************************************************************************
typedef uint8_t DeviceType_t;
   #define AQUAERO5_LT              0
   #define AQUAERO5_PRO             1
   #define AQUAERO5_XT              2
   #define AQUADUCT_MK4_360         4
   #define AQUADUCT_MK4_720         5 
   #define AQUADUCT_MK5_360         6
   #define AQUADUCT_MK5_720         7 

//*****************************************************************************
//* Name:
//*   DeviceCapabilities
//* Description:
//*   Hardware capabilities
//*****************************************************************************
typedef union {
   uint16_t data;
   struct {
      uint16_t HasDisplay :1;
      uint16_t HasKeys :1;
      uint16_t HasTouch :1;
      uint16_t HasIrReciver :1;
   } Bits;
} DeviceCapabilities;

//*****************************************************************************
//* Name:
//*     DeviceInfo
//* Description:
//*     Alle Infos des Gerätes
//*****************************************************************************
struct DeviceInfo {
   uint16_t serial[2];              //serial number - 2x 16 bit value
   uint16_t firmware;               //firmware version
   uint16_t bootloader;             //bootloader id
   uint16_t hardware;               //hardware id
   uint32_t uptime;                 //uptime poweron -> now
   uint32_t uptimeTotal;            //uptime sum
   DeviceStatus status;
   SystemLockControl lockControl;   //gerätesperren verwalten
   DeviceType_t deviceType;
   DeviceCapabilities capabilities;
};

//*****************************************************************************
//* Name:
//*   AlarmLevel_t
//* Description:
//*   Alarm priorität, 0..7
//*   0 = normalzustand, 0..7 alarm levels
//*****************************************************************************
typedef uint8_t AlarmLevel_t;
   #define ALARM_LEVEL_NORMAL       0
   #define ALARM_LEVEL_WARN         1
   #define ALARM_LEVEL_ALARM        2
   
   //*****************************************************************************
//* Name:
//*   AquabusStatus
//* Description:
//*   connected bus devices
//*****************************************************************************
typedef union {
   uint32_t data;
   struct {
      uint32_t aquastream1 :1;   //0x0001
      uint32_t aquastream2 :1;   //0x0002
      
      uint32_t poweradjust1 :1;  //0x0004
      uint32_t poweradjust2 :1;  //0x0008
      uint32_t poweradjust3 :1;  //0x0010
      uint32_t poweradjust4 :1;  //0x0020
      uint32_t poweradjust5 :1;  //0x0040
      uint32_t poweradjust6 :1;  //0x0080
      uint32_t poweradjust7 :1;  //0x0100
      uint32_t poweradjust8 :1;  //0x0200
      
      uint32_t mps1 :1;          //0x0400
      uint32_t mps2 :1;          //0x0800
      uint32_t mps3 :1;          //0x1000
      uint32_t mps4 :1;          //0x2000
      
      uint32_t rtc :1;           //0x4000
      uint32_t aquaero5slave :1; //0x8000  
      
   } Bits;
} AquabusHighStatus;

typedef union {
   uint32_t data;
   struct {
      uint32_t multiswitch1 :1;  //0x0001
      uint32_t multiswitch2 :1;  //0x0002
      uint32_t tubemeter :1;     //0x0004      
   } Bits;
} AquabusLowStatus;

//*****************************************************************************
//* Name:
//*   PowerConsumptionData
//* Description:
//*  power consumption measurement
//*****************************************************************************
struct PowerConsumptionData{
   int16_t flow;                 //attached flow value
   int16_t sensor1;              //sensor 1
   int16_t sensor2;              //sensor 2
   int16_t detaT;                //absolute differenztemperatur zw. s1 und s2
   int16_t power;                //leistung die berechnet wurde
   int16_t rth;                  //wärmewiderstand
};

//*****************************************************************************
//* Name:
//*   FanData
//* Description:
//*   Aktuelle Lüfter Daten
//*****************************************************************************
struct FanData{
   int16_t rpm;                     //fan rpm
   int16_t power;                   //fan power
   int16_t voltage;                 //fan voltage
   int16_t current;                 //fan current
   int16_t performance;				//power draw in W
   int16_t torque;					//torque in Nm
};

//*****************************************************************************
//* Name:
//*   AquastreamMode_t
//* Description:
//*   aquastream xt mode
//*****************************************************************************
typedef uint8_t AquastreamMode_t;
#define AQUASTREAM_AUTOFREQ            0     //automatische frequenzermittlung
#define AQUASTREAM_MANUALFREQ          1     //manuelle frequenzvorgabe
#define AQUASTREAM_DEARATION           2     //entlüftungs modus
#define AQUASTREAM_OFFLINE             255   //entlüftungs modus

//*****************************************************************************
//* Name:
//*   AquastreamStatus
//* Description:
//*   Pump state
//*****************************************************************************
typedef union {
   uint8_t data;
   struct {
      uint8_t available :1; //pumpe available = 1
      uint8_t alarm :1; 	//pump has errors
   } Bits;
} AquastreamStatus;

//*****************************************************************************
//* Name:
//*   AquastreamData
//* Description:
//*   Aquastream Daten
//*****************************************************************************
struct AquastreamData{
   AquastreamStatus status;
   AquastreamMode_t mode;        //
   int16_t frequency;            // frequencey in HZ
   int16_t voltage;              //	voltage in 1/100V
   int16_t current;              // curretn in mA
};

//*****************************************************************************
//* Name:
//*   MultiswitchData
//* Description:
//*   Multiswitch Datenstruktur
//*****************************************************************************
struct MultiswitchData{
   uint8_t ampOutputs;
   uint8_t ledOutputs;
   uint8_t relay;
};

//*****************************************************************************
//* Name:
//*   SoftwareSensorConfig_t
//* Description:
//*   Temperatur alarm konfiguration
//*****************************************************************************
typedef uint8_t AquabusSensorDataType_t;
   #define AQUABUSSENSOR_NONE       0  //no sensor
   #define AQUABUSSENSOR_FLOW       1  //flow
   #define AQUABUSSENSOR_PRESSURE   2  //pressure
   #define AQUABUSSENSOR_LEVEL      3  //fill level
   #define AQUABUSSENSOR_RPM        4  //rpm
   #define AQUABUSSENSOR_TEMP_EXT   5  //temperature external   
   #define AQUABUSSENSOR_TEMP_INT   6  //temperature internal
   #define AQUABUSSENSOR_PUMP       7  //pump power

//*****************************************************************************
//* Name:
//*   PowerConsumptionData
//* Description:
//*  Daten der Leistungsmessung
//*****************************************************************************
struct MpsData{
   int16_t power;
   AquabusSensorDataType_t dataType;
   int16_t sensorData;   
};

//*****************************************************************************
//* Name:
//*   OutData
//* Description:
//*   All out Data
//*****************************************************************************
struct OutData {
   uint8_t reportId;                //usb Report ID
   uint32_t timestamp;              //aktuelle zeit + datum im 32bit unix format sek ab zeitpunkt x.
   uint16_t stuctureVersion;        //struktur Version
   
   struct DeviceInfo devInfo;       //device info (serial...)
   uint32_t lastSettingsUpdateTime; //lastKeypress time
   
   struct GuiState lcdstate;        //(20 bytes)
   AlarmLevel_t alarmlevel;         //aktuelles alarm level
   uint8_t actualProfile;           //aktuelles Profil
   AquabusHighStatus aquabusHigh;   //aquabus status highspeed
   AquabusLowStatus aquabusLow;     //aquabus status lowspeed
   
   uint16_t adcRAW[20];             //adc raw werte
   int16_t temperatures[64];        //64 temperatures
   uint32_t rawRpmFlow[5];          //impulse rpm times in 1/100ms == 0.01e10-3s 
   int16_t flow[14];                //2xinternal flow + 8xpoweradjust + 4xmps flow
   struct PowerConsumptionData powerConsumption[4];     //power consumption data
   int16_t level[4];                //fill level sensor
   int16_t humidity[4];             //humidity sensor data (not available yet)
   int16_t conductivity[4];         //water quality (aquaduct only)
   int16_t pressure[4];             //pressure sensors (mps)
   
   int16_t tacho;                   //current rpm output
   struct FanData fans[12];  		//current fan data
   struct AquastreamData aquastream[2];//aquastream xt   
   int16_t ledPower[3];             //led output
   int16_t switchingOutputs[3];     //schaltausgänge (relay + PWM out1/2)
   struct MultiswitchData multiswitch[2]; //multiswitch datenstruktur
   struct MpsData mps[4];           //mps data
   
   int16_t twopoint[16];            //2controller outputs
   int16_t constData[32];           //constant preset outputs
   int16_t rgbLed[3];               //rgb controller outputs
   int16_t setPoint[8];             //setpoint controller
   int16_t curve[4];                //curve controller
   .
   .
   .
};