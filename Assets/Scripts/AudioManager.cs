using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;
	public static AudioManager Instance
	{
		get{return _instance;}
	}

	void Awake()
	{
		_instance=this;
	}

	public float minPitch = 0.9f;
	public float maxPitch = 1.1f;

	public AudioSource efxSouce;

	public AudioSource bgSource;

	public void RandomPlay(params AudioClip[] clips)
	{
		float pitch=Random.Range(minPitch,maxPitch);
		int index = Random.Range(0,clips.Length);
		AudioClip clip =clips[index];

		efxSouce.clip=clip;
		efxSouce.pitch=pitch;
		efxSouce.Play();

	}

	public void StopBgMusic()
	{
		bgSource.Stop();
	}
}
