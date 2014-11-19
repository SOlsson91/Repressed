
function Update () {

		var cam = camera.main;
		
		var godRays = cam.GetComponent("GodRays");
		
		var sunPos = godRays.sun.transform;
		
		sunPos.position = cam.transform.position + (cam.transform.forward * 1000);
	
}