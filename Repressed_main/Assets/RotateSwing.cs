
using UnityEngine;
using System.Collections;


public class RotateSwing : MonoBehaviour
{

		public float angle = 5.0f;
		public float speed = 2000f;
		
		Quaternion qStart, qEnd;
		
		void Start () {
			qStart = Quaternion.AngleAxis ( angle, Vector3.right);
			qEnd   = Quaternion.AngleAxis (-angle, Vector3.right);
		}
		
		void Update () {
			transform.rotation = Quaternion.Lerp (qStart, qEnd, (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f);
		}

	
}