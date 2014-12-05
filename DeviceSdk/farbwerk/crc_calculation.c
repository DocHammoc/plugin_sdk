const uint16_t crc_table[16] = {0,1,1,0,1,0,0,1,1,0,0,1,0,1,1,0};

//*****************************************************************************
//* Name:
//*   docrc16
//* In:
//*   Data
//* Out:
//*   CRC
//* Description:
//*   CRC16 polynom (x^16 + x^15 + x^2 + 1)
//*****************************************************************************
uint16_t docrc16(uint16_t data,uint16_t crc)
{
   data = (data ^ (crc & 0xff)) & 0xff;
   crc >>= 8;
   if ((crc_table[data & 0xf] ^ crc_table[data >> 4]) > 0)
     crc ^= 0xc001;
   data <<= 6;
   crc   ^= data;
   data <<= 1;
   crc   ^= data;
   return crc;
}

//*****************************************************************************
//* Name:
//*   docrc16
//* In:
//*   *data: input data buffer
//*   count: data count
//* Out:
//*   crc16
//* Description:
//*   CRC16 polynom (x^16 + x^15 + x^2 + 1)
//*   crc type: crc16
//*   crc polynom: 0x8005
//*   crc init: 0xffff
//*   crc xor: 0xffff
//*****************************************************************************
uint16_t crc16(uint8_t *data, uint16_t count)
{
   uint16_t crc, tmp;   
   crc = 0xffff;
   while(count > 0){
      count--;
      tmp = *data;
      crc = docrc16(tmp, crc);
      *data++;    
   }
   crc ^= 0xffff;
   return(crc);
}