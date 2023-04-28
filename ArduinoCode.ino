long fsrAnalogPin0 = 0; // first FSR is connected to analog 0
long fsrAnalogPin1 = 1; // second FSR is connected to analog 1
long fsrReading0;      // the analog reading from the FSR resistor divider
long fsrReading1;      // the analog reading from the FSR resistor divider
bool start = false;



void setup(void) {
  Serial.begin(9600);   // We'll send debugging information via the Serial monitor
}



void loop(void) {
  fsrReading0 = analogRead(fsrAnalogPin0);
  fsrReading1 = analogRead(fsrAnalogPin1);
  //Serial.println(fsrReading0 - fsrReading1);

  if (!start && (fsrReading0 - fsrReading1 > 70 || fsrReading0 - fsrReading1 < -70)){
    start = true;
  }

  if (start) {
    Serial.println(fsrReading0 - fsrReading1);
  }
  delay(19);
}
