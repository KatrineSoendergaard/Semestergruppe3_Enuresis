#ifndef WEIGHTSENSOR_H 
#define WEIGHTSENSOR_H

#include "HX711.h"

class WeightSensor{
  public:
    WeightSensor(int doutPin, int sckPin);
    void begin();
    float getWeight();
    void tare();

  private:
    HX711 scale;
    int dout;
    int sck;
};

#endif