/*
 *  Use software serial to talk to serial/UART connected device
 *  What ever is entered in the serial monitor is sent to the connected device
 *  Anything received from the connected device is copied to the serial monitor
*  User input is echo'd to the serial monitor
 */

#include <SoftwareSerial.h>
SoftwareSerial softSerial(2, 3); // RX, TX
 
char c=' ';
boolean NL = true;
 
void setup() 
{
    Serial.begin(9600);
    Serial.print("Sketch:   ");   Serial.println(__FILE__); // Udskriver navnet på programfil
    Serial.print("Uploaded: ");   Serial.println(__DATE__); // Udskriver dato, hvor koden sidst blev uploadet

    softSerial.begin(9600); //Softwarebaseret seriel port, der taler med BT-modul via digitale ben (D2 og D3)
    Serial.println("softSerial started at 9600");

    Serial.println("Set line endings to 'Both NL & CR'");
}
 
void loop()
{
    // Read from the UART module and send to the Serial Monitor
    if (softSerial.available())
    {
        c = softSerial.read();
        Serial.write(c); 
    }
    
    // Read from the Serial Monitor and send to the UART module
    if (Serial.available())
    {
        c = Serial.read();
        
        // do not send line end characters to the HM-10
        if (c!=10 & c!=13 ) {   softSerial.write(c); }

        // Echo the user input to the main window. 
        // If there is a new line print the ">" character.
        if (NL) { Serial.print("\r\n>");  NL = false; }
        Serial.write(c);
        if (c==10) { NL = true; }
    }

    int weight = 350;
    String data = "Weight: " + String(weight) + " ml";

    softSerial.println(data);
    Serial.println("Sendt til Bluetooth: " + data);

    delay(20000);
    
} 