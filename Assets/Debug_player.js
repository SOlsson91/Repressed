#pragma strict

 var cMotor: CharacterMotor; // reference to the CharacterMotor script
 var roof : GameObject;
 var speed : int; 
 function Start(){ // get the CharacterMotor script at Start:
     cMotor = GetComponent(CharacterMotor);
     roof = GameObject.FindGameObjectWithTag("Deactivate");
     speed = 10;
 }
 
 function Update(){ // move player upwards while F is pressed
     if (Input.GetKey(KeyCode.Space)){
         cMotor.SetVelocity(Vector3.up*speed);
         
     }
     else if(Input.GetKey(KeyCode.LeftShift))
     {
     	cMotor.SetVelocity(Vector3.down*speed);
  
     }
     else
     {
     	cMotor.SetVelocity(Vector3.zero);
     }
     
     if(Input.GetKeyDown(KeyCode.F))
     {
     	if(roof.activeInHierarchy)
     	{
     		roof.SetActive(false);
     	}
     	else
     	{
     		roof.SetActive(true);
     	}
     }
     if(Input.GetKey(KeyCode.T))
     {

     	speed ++;
     
     }
     else if(Input.GetKey(KeyCode.R))
     {
     	speed --;
     }
 }
 
 // This do-nothing function is included just to avoid error messages
 // because SetVelocity tries to call it
 
 function OnExternalVelocity(){
 }