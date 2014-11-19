using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
[AddComponentMenu("Image Effects/Godrays")]
[ExecuteInEditMode]
public class GodRaysReduction : MonoBehaviour
{
	public bool detailControl = true; // Change shaders by user input
	
	public int targetSize = 512;
	
	public int PassCount = 3;
	
	public bool qaulityFiltering = true;
	
	public bool qaulityBuffers = true;
	private bool canUseQaulityBuffers = false; // Initialized by start
	
	public bool smoothAddition = true;
	
	private Shader shader = Shader.Find("Godrays/ReductionShader"); // Default shader
	
	private Material m_Material; // Material used to render godrays
	
	public float weight = 1.5F; // Factor to multiply godray effect
	public float exposure = 0.9F;// Exposure to darken the rest of the environment
	public float density = 0.5F;// Density of Godray samples, closer = cleaner, shorter rays
	public float decay = 0.9F; // Falloff for Godrays
	
	public Transform sun; // Position of sun in world, should be far off
	
	public bool debugGodrays = false;
	
	/* Godray rendering works by stretching areas of the screen away from the visible sun position,
	This technique inherently fails when the sun is behind the camera, or in some weird position-
	To compensate for these issues, autobreak kills the effect when not looking at the sun
	If you have an exposure != 1, this can be visibily jarring, hence adapative dimming
	Adapative dimming fades the effect in and out as you look at or away from the sun- 
	When coupled with an exposure < 1, you will get an HDR like effect, which is quite pretty. */
	
	public bool autoBreak = true; // Disable Godrays when they would no longer make sense
	public bool adaptiveDimming = true; // Fade godrays as they fall out of view
	public float dimmingFalloff = 1.0F; // Falloff for grazing angles 
	// Vert factor delays the fading for godrays when looking up or down, 0.4-1.0 tend to work well
	// If the number is too low, godrays will appear to seperate and fragment at extreme angles
	
	public float vertFactor = 0.5F; // Factor for fadeoff of godrays vertically
	
	public enum detailLevel {Min,Low,Med,High}; // Used to easily allow other scripts to change detail
	
	public detailLevel detail; // Our current detail level, of the four current details
	
	private float scroll; // Used with mousewheel control to change detail by scrolling
	
	private bool openGL;
	
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
			
			if (dimWeight != 1)
				dimWeight = Mathf.Pow(dimWeight,dimmingFalloff);
			
			setting = new Vector4 (weight * dimWeight, Mathf.Lerp(1,exposure,dimWeight), density * dimWeight, decay);
		}
		else
			setting = new Vector4 (weight, exposure, density, decay);
		
		// Where is the sun in screenspace?
		
		Vector3 sunCam = camera.WorldToViewportPoint (sun.transform.position);
		
		material.shader = shader;

		if(openGL) // renderer specific settings
		{ // OpenGL requires the sun-camera values to be scaled by the screen area
			sunCam *= targetSize;
			//sunCam.x *= camera.pixelWidth;
			//sunCam.y *= camera.pixelHeight;
		}
		else // Uaing DirectX
		{ // DirectX doesn't properly blend the brightness between passes, so weight needs to be increased proportionately
			setting.x *= PassCount; // Scale weight by passcount
		}
		
		// Shader has since been optimized to read from a float4, so paramaters are combined
		material.SetVector("_sunpos", (Vector4)sunCam);
		material.SetVector("_params", setting);
		
		// Now that the material is configured, we can render it into the buffer
		
		RenderTextureFormat renderFormat = RenderTextureFormat.ARGB32;
		if (qaulityBuffers)
			if (canUseQaulityBuffers)
				renderFormat = RenderTextureFormat.ARGBHalf;
		
		// Create downsized buffer to store a reduction of the main texture
		RenderTexture mainBuffer = RenderTexture.GetTemporary(targetSize,targetSize,0,renderFormat);
		
		if (qaulityFiltering)
		{
			if (mainBuffer.isPowerOfTwo)
			{
				mainBuffer.useMipMap = true;
				mainBuffer.filterMode = FilterMode.Trilinear;
			}
			else
				mainBuffer.filterMode = FilterMode.Bilinear;
		}
		
		//mainBuffer.filterMode = FilterMode.Bilinear;
		
		// Create the second buffer to render the effects into
		//RenderTexture outBuffer = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGB32); 
		RenderTexture outBuffer = RenderTexture.GetTemporary(targetSize, targetSize, 0, renderFormat); 
		
		Graphics.Blit(source,mainBuffer); // Move the source to the main buffer
		
		RenderTexture summationBuffer = RenderTexture.GetTemporary(source.width, source.height, 0, renderFormat);
		
		float passCountRecip = (1F/PassCount);
		
		material.SetFloat("_passCountRecip",passCountRecip);
		
		if (passCountRecip <= 0.01) // No way are we going to render over a hundred passes, that would be silly.
			return;
		
		// Render the image using our godray shader, offsetting the sample location slightly each time
		for (float layerOffset = 0.0F; layerOffset < 1.0F; layerOffset += passCountRecip)
		{
			//Debug.Log("Running loop with offset " + layerOffset.ToString() + " and Recip " + passCountRecip.ToString());
			material.SetFloat("_passOffset",layerOffset);
			Graphics.Blit (mainBuffer, outBuffer, material); // render pass
			
			// Summ it into the buffer
			//Graphics.Blit (outBuffer, summationBuffer, ImageEffects.GetBlitMaterial(BlendMode.AddSmoooth));
			if (layerOffset > 0)
				if (smoothAddition)
					ImageEffects.Blit(outBuffer, summationBuffer, BlendMode.AddSmoooth);
				else
					ImageEffects.Blit(outBuffer, summationBuffer, BlendMode.Add);
			else
				Graphics.Blit(outBuffer, summationBuffer);
		}
		
		//Graphics.Blit (source, outBuffer, ImageEffects.GetBlitMaterial(BlendMode.AddSmoooth)); // Add the source and outbuffer together
		if (!debugGodrays)
			ImageEffects.Blit(source, summationBuffer,BlendMode.Add);
		//Graphics.Blit(source,summationBuffer,ImageEffects.GetBlitMaterial(BlendMode.Add));
		Graphics.Blit(summationBuffer,dest);
		//Graphics.Blit (summationBuffer, dest); // Now finally update the destination 
		
		// Release the temporary buffers
		
		RenderTexture.ReleaseTemporary(summationBuffer);
		RenderTexture.ReleaseTemporary(mainBuffer);
		RenderTexture.ReleaseTemporary(outBuffer);
	}
	
	void Start () // Errors
	{	
		if((SystemInfo.graphicsDeviceVersion).Contains("OpenGL"))
			openGL = true;
		
		if((SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))) // Can we use high-qaulity buffers?
			canUseQaulityBuffers = true;
		
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