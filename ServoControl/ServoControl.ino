#include <Servo.h>
Servo myservo;
int pos=0;
int x;
const int trigPin=5,echoPin=6;
long dis;
long tim;
void setup()
{
  pinMode(trigPin,OUTPUT);
  pinMode(echoPin,INPUT);
 Serial.begin(9600);

 myservo.attach(9);
 

}

void loop()
{
  digitalWrite(trigPin,LOW);
  delayMicroseconds(5);
  digitalWrite(trigPin,HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin,LOW);
  tim=pulseIn(echoPin,HIGH);
  dis=tim/2/29.1;
  if(dis>200)
  {
    dis=200;
  }
  Serial.println(dis);
  
  
  if(Serial.available())
  {
    pos=Serial.read();
    if(pos>0)
    {
      x=pos;
    }
  }
  myservo.write(x);

}
