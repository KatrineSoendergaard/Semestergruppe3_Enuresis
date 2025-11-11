#ifndef DISPLAY_H 
#define DISPLAY_H

#include <SPI.h> //Kræves af Adafruit-biblioteket for at kompilere korrekt
#include <Wire.h> //Kommunikation med OLED (via SDA/SCL)
#include <Adafruit_SH110X.h>
#include <Adafruit_GFX.h>


class Display{
  public:
    Display();
    void begin();
    void displayWeight(float weight, bool connected);
};

#endif