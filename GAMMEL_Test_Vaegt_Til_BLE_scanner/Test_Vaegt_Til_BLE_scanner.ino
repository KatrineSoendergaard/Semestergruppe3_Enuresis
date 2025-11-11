#include <SoftwareSerial.h>
SoftwareSerial softSerial(2, 3); // RX, TX

char c = ' ';
boolean NL = true;

void setup() 
{
  Serial.begin(9600);
  Serial.println("HM-10 Bluetooth vægt sender");
  Serial.println("Tryk på en vilkårlig tast for at sende vægten.\n");

  softSerial.begin(9600); // Kommunikerer med HM-10
  Serial.println("softSerial started at 9600");
}

void loop()
{
  // Læs fra HM-10 (Bluetooth) og vis på Serial Monitor
  if (softSerial.available()) {
    c = softSerial.read();
    Serial.write(c);
  }

  // Når der trykkes på en tast i Serial Monitor
  if (Serial.available()) {
    char userInput = Serial.read(); // læser tastetryk
    // Ignorer linjeskift
    if (userInput != '\n' && userInput != '\r') {
      int weight = 366; // Ændre i vægten
      String data = "Weight: " + String(weight) + " ml";

      // Send til Bluetooth
      softSerial.println(data);
      Serial.println("Sendt til Bluetooth: " + data);
    }
  }
}
