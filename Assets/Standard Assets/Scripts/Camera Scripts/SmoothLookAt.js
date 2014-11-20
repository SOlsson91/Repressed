var target = GameObject.FindGameObjectWithTag("Player");
var damping = 6.0;
var smooth = false;

@script AddComponentMenu("Camera-Control/Smooth Look At")

function LateUpdate () {
	if (target) {
		if (smooth)
		{
			// Look at and dampen the rotation
			var rotation = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		}
		else
		{
			// Just lookat
		    transform.LookAt(target.transform.position);
		}
	}
}

function Start () {
	// Make the rigid body not change rotation
   //	if (GetComponent.<Rigidbody>())
		//GetComponent.<Rigidbody>().freezeRotation = true;
}