#include "Display.h"

#define I2C_ADDRESS 0x3c // Typisk I2C-adresse for OLED. En Unik Adresse som specifikt hører til SCL og SDA. 
#define SCREEN_WIDTH  128 //1.3" SH1107 er 64x128 (vertikal)
#define SCREEN_HEIGHT 64
#define OLED_RESET -1 //// Ingen separat reset-pin på I2C-modulet. Springer reset signalet over, da det ikke findes. Er forbundet internt. 

Adafruit_SH1106G display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET); // Opretter display objekt, som bruges til at styre skærmen. 

Display::Display(){
}

void Display::begin(){
  Wire.begin(); 
  Wire.setClock(100000);      // 100 kHz = stabilt
  delay(200);                 // lad OLED få strøm

  display.begin(I2C_ADDRESS, true); // Address 0x3C default

  display.setRotation(0);           // 0 eller 2 alt efter orientering

  display.oled_command(0xA0);
  display.oled_command(0xC8);

  display.clearDisplay();
  display.setContrast(0x2F);        // dæmpet kontrast for roligt billede
  // Tekstopsætning
  display.setTextSize(2);           // lidt større tekst
  display.setTextColor(SH110X_WHITE);
  Serial.println("Display setup finished");
  display.setCursor(10, 20);        // placering (x=10, y=20)
  display.println("Weight is ");
  display.setCursor(30, 45);
  display.println("ready");
  display.display();
}

void Display:: displayWeight(float weight){
  display.clearDisplay();
  display.setCursor(10, 20);        // placering (x=10, y=20)
  display.println("Weight: ");
  display.setCursor(30, 45);
  display.println(String(weight) + " g");
  
  display.display();
  Serial.println("Vægt vist på display");
  delay(2000);
  display.clearDisplay();
}

 