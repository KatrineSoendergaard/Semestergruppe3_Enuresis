#ifndef BLUETOOTH_H //tjekker om BLUETOOTH_H allerede er defineret. Dette og nedenstående forhindrer, at header-filen inkluderes flere gange. 
#define BLUETOOTH_H //definerer BLUETOOTH_H

#include <Arduino.h> //inkluderer Arduino typer og funktioner (fx String)

class Bluetooth{
  public:
  Bluetooth(HardwareSerial &serialPort = Serial1); //constructor med default-parameter. & betyder reference
  void begin(int baud); //metode til at opsætte baudrate 
  void sendWeight(float weight); //metode til at sende vægt via Bluetooth

  public:
    HardwareSerial &btSerial;
};

#endif //markerer afslutningen på den betingede kodeblok, der blev startet med #ifndef