#include "Bluetooth.h"
#include "WeightSensor.h"
#include "Display.h"

Bluetooth bt;
WeightSensor weightSensor(4,5); //HX711 pins (DOUT, SCK)
Display display1;

void setup() {
  // USB Serial til debugging
  Serial.begin(9600);
  Serial.println("Serial started at 9600");

  bt.begin(9600);
  weightSensor.begin();
  display1.begin();

  Serial.println("Tryk på en vilkårlig tast i Serial Monitor for at sende vægten.\n");

  delay(1000);
}

void loop() {
  float weight = weightSensor.getWeight();
  Serial.print("Average weight:\t");
  Serial.println(weight); // printer vægten
  display1.displayWeight(weight,true); //viser vægten på display

  //Send vægt ved tastetryk i Serial Monitor
  if (Serial.available()) {
    char userInput = Serial.read(); //læser tastetryk
    //ignorer linjeskift
    if (userInput != '\n' && userInput != '\r') {
      bt.sendWeight(weight); //sender vægten over bluetooth
    }
  }

}



