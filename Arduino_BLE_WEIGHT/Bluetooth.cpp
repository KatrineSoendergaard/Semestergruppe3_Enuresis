#include "Bluetooth.h"

Bluetooth:: Bluetooth(HardwareSerial &serialPort) : btSerial(serialPort) {}

void Bluetooth::begin(int baud){
  btSerial.begin(baud);
  Serial.println("Serial1 (HM-10) started at " + String(baud));
}

void Bluetooth::sendWeight(float weight) {
  String data = "Urin: " + String(weight, 1);
  btSerial.println(data); // send til HM-10
  Serial.println("Sendt via Bluetooth: " + data); // debug
}


