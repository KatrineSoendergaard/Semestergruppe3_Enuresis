#include "WeightSensor.h"

WeightSensor::WeightSensor(int doutPin, int sckPin) : dout(doutPin),sck(sckPin){}

void WeightSensor::begin(){
  Serial.println("Initialiserer vægten");
  scale.begin(dout, sck);
  scale.set_scale(805.3474);
  scale.tare(); // nulstil vægten
  Serial.println("Vægten er nulstillet/taret og klar til at veje");
}

float WeightSensor::getWeight(){
  return scale.get_units(10);
}

