using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
[AddComponentMenu("Image Effects/Godrays")]
public class GodRays : MonoBehaviour
{
	public bool detailControl = true; // Change shaders by user input
	
	private Shader shader = Shader.Find("Godrays/PS3M"); // Default shader
	
	public Shader[] shaders = new Shader[4] // Our array of shaders
	{ 	Shader.Find("Godrays/PS2"), 
		Shader.Find("Godrays/PS3L"), 
		Shader.Find("Godrays/PS3M"), 
		Shader.Find("Godrays/PS3H") };
	
	private Material m_Material; // Material used to render godrays
	
	public float weight = 1.5F; // Factor to multiply godray effect
	public float exposure = 0.9F;// Exposure to darken the rest of the environment
	public float density = 0.5F;// Density of Godray samples, closer = cleaner, shorter rays
	public float decay = 0.9F; // Falloff for Godrays
	
	public Transform sun; // Position of sun in world, should be far off
	
	/* Godray rendering works by stretching areas of the screen away from the visible sun position,
	This technique inherently fails when the sun is behind the camera, or in some weird position-
	To compensate for these issues, autobreak kills the effect when not looking at the sun
	If you have an exposure != 1, this can be visibily jarring, hence adapative dimming
	Adapative dimming fades the effect in and out as you look at or away from the sun- 
	When coupled with an exposure < 1, you will get an HDR like effect, which is quite pretty. */
	
	public bool autoBreak = true; // Disable Godrays when they would no longer make sense
	public bool adaptiveDimming = true; // Fade godrays as they fall out of view
	
	// Vert factor delays the fading for godrays when looking up or down, 0.4-1.0 tend to work well
	// If the number is too low, godrays will appear to seperate and fragment at extreme angles
	
	public float vertFactor = 0.5F; // Factor for fadeoff of godrays vertically
	
	public enum detailLevel {Min,Low,Med,High}; // Used to easily allow other scripts to change detail
	
	public detailLevel detail; // Our current detail level, of the four current details
	
	private float scroll; // Used with mousewheel control to change detail by scrolling
	
	private bool openGL;
	
	void updateDetail()
	{
		if (detailControl) // Mousewheel Control is on?
		{
			// Store it, the * 4 makes it change faster
			if (Input.GetButtonDown("Fire2"))
				++scroll;
			
			// Clamp to our range of shader indexes
			scroll = Mathf.Repeat(scroll,3.0F);
			
			// And set the shader to it
			shader = shaders[(int)scroll];
			
			return;
		}
		
		switch(detail) // If we arn't controlling via mousewheel, use detail setting
		{	
			case detailLevel.Min:
				shader = shaders[0];
				break;
			case detailLevel.Low:
				shader = shaders[1];
				break;
			case detailLevel.Med:
				shader = shaders[2];
				break;
			case detailLevel.High:
				shader = shaders[3];
				break;
			default:
				return;
		}
	}
	
	void Update() // About to draw
	{
		updateDetail(); // Get out level of detail
	}
	
	void OnRenderImage (RenderTexture source, RenderTexture dest) // Drawing
	{
		Vector4 setting; // Settings for shader
		
		if (autoBreak) // Autobreak fork
		{
			Vector3 offset = sun.transform.position - transform.position;
			Vector3 forward = transform.forward;
			
			forward.y *= vertFactor;
			offset.y *= vertFactor;
			
			float sunAngle = Vector3.Angle(forward, offset);
		
			float fovFactor = (camera.fieldOfView / 2);
		
			if (90 <= (sunAngle + fovFactor)) // The point where it really no longer matters
			{
				Graphics.Blit(source,dest);
				return;
			}
			
			float dimWeight;
			
			if (adaptiveDimming) // Are we adapatively dimming?
			{
				dimWeight = 1 - ((Mathf.Max(sunAngle,fovFactor) - fovFactor) / fovFactor);
				
			}
			else
				dimWeight = 1;
			
			setting = new Vector4 (weight * dimWeight, Mathf.Lerp(1,exposure,dimWeight), density * dimWeight, decay);
		}
		else
			setting = new Vector4 (weight, exposure, density, decay);
		
		
		// Where is the sun in screenspace?
		
		Vector3 sunCam = camera.WorldToViewportPoint (sun.transform.position);
		
		material.shader = shader;

		if(openGL) // renderer specific settings
		{ // OpenGL requires the sun-camera values to be scaled by the screen area
			sunCam.x *= camera.pixelWidth;
			sunCam.y *= camera.pixelHeight;
		}
		else // Uaing DirectX
		{ // DirectX doesn't properly blend the brightness between passes, so weight needs to be increased proportionately
			setting.x *= material.passCount; // Scale weight by passcount
		}
		
		// Shader has since been optimized to read from a float4, so paramaters are combined
		material.SetVector("_sunpos", (Vector4)sunCam);
		material.SetVector("_params", setting);
		
		// Render the image using the motion blur shader
		Graphics.Blit (source, dest, material);
	}
	
	void Start () // Errors
	{	
		if((SystemInfo.graphicsDeviceVersion).Contains("OpenGL"))
			openGL = true;
		
		// Is the sun assigned?
		if (sun == null)
		{
			Debug.Log("Please assign the sun-transform");
			enabled = false;
			return;
		}
		
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			Debug.Log("System doesn't support image effects.");
			enabled = false;
			return;
		}
		
		// Disable the image effect if the shader can't
		// run on the users graphics card
		// Shaders all fallback, so this will only occur for <PS2.0 cards
		if (!shader.isSupported)
			enabled = false;
	}

	protected Material material {
		get {
			if (m_Material == null) {
				m_Material = new Material (shader);
				m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			return m_Material;
		} 
	}
	
	protected void OnDisable() {
		if( m_Material ) {
			DestroyImmediate( m_Material );
		}
	}
}