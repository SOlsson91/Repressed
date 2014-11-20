using UnityEngine;
using System.Collections;
/* Discription: Music Options
* Used for changing the volume of the sound
*
* Created by: Sebastian Olsson 15/04-14
* Modified by:
*/


//TODO: Funkar inte som den är nu, ska kopplas till eventuella options
public class MusicOptions : MonoBehaviour
{
	#region PublicVariableMembers
	[Range(0,1)] private float m_MasterVolume = 1.0f;
	[Range(0,1)] private float m_MusicVolume = 1.0f;
	[Range(0,1)] private float m_SoundVolume	= 1.0f;
	[Range(0,1)] private float m_VOVolume = 1.0f;
	#endregion
	private FMOD.Studio.MixerStrip masterBus;
	private FMOD.Studio.MixerStrip musicBus;
	private FMOD.Studio.MixerStrip soundBus;
	private FMOD.Studio.MixerStrip VOBus;
	void Start ()
	{
		FMOD.GUID masterGuid;
		FMOD.Studio.System masterSystem = FMOD_StudioSystem.instance.System;
		ERRCHECK( masterSystem.lookupID("bus:/", out masterGuid) );
		ERRCHECK( masterSystem.getMixerStrip(masterGuid, FMOD.Studio.LOADING_MODE.BEGIN_NOW, out masterBus) );
		FMOD.GUID musicGuid;
		FMOD.Studio.System musicSystem = FMOD_StudioSystem.instance.System;
		ERRCHECK (musicSystem.lookupID ("bus:/Music", out musicGuid));
		ERRCHECK (musicSystem.getMixerStrip (musicGuid, FMOD.Studio.LOADING_MODE.BEGIN_NOW, out musicBus));	
		FMOD.GUID VOGuid;
		FMOD.Studio.System VOSystem = FMOD_StudioSystem.instance.System;
		ERRCHECK (VOSystem.lookupID ("bus:/VO", out VOGuid));
		ERRCHECK (VOSystem.getMixerStrip (VOGuid, FMOD.Studio.LOADING_MODE.BEGIN_NOW, out VOBus));	
		FMOD.GUID soundGuid;
		FMOD.Studio.System soundSystem = FMOD_StudioSystem.instance.System;
		ERRCHECK (soundSystem.lookupID ("bus:/Sound", out soundGuid));
		ERRCHECK (soundSystem.getMixerStrip (soundGuid, FMOD.Studio.LOADING_MODE.BEGIN_NOW, out soundBus));	
	}
	void Update ()
	{
		//Debug.Log (getVolume (masterBus));
		setVolume ("master", m_MasterVolume);
		//Debug.Log (getVolume (musicBus));
		//setVolume (musicBus, m_MusicVolume);
		////Debug.Log (getVolume (soundBus));
		//setVolume (soundBus, m_SoundVolume);
		////Debug.Log (getVolume(VOBus));
		//setVolume (VOBus, m_VOVolume);
	}
	public void setVolume(/*FMOD.Studio.MixerStrip p_Bus*/ string name, float p_MasterVolume)
	{
		switch (name)
		{
		case "master":
			ERRCHECK(masterBus.setFaderLevel(p_MasterVolume));
			break;
		case "music":
			ERRCHECK(musicBus.setFaderLevel(p_MasterVolume));
			break;
		case "sound":
			ERRCHECK(soundBus.setFaderLevel(p_MasterVolume));
			break;
		case "voiceOver:":
			ERRCHECK(VOBus.setFaderLevel(p_MasterVolume));
			break;
		default:
			break;
		}
	}
	public float getVolume(FMOD.Studio.MixerStrip p_Bus)
	{
		float volume;
		ERRCHECK (p_Bus.getFaderLevel (out volume));
		return volume;
	}
	FMOD.RESULT ERRCHECK(FMOD.RESULT result)
	{
		FMOD.Studio.UnityUtil.ERRCHECK(result);
		return result;
	}
}