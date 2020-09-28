using UnityEngine.Audio;
using UnityEngine;
using System;

public class PlaySFX : MonoBehaviour
{
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.audioSrc = gameObject.AddComponent<AudioSource>();
            s.audioSrc.clip = s.clip;
            s.audioSrc.volume = s.volume;
            s.audioSrc.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    public void PlaySound(string name)
    {
      
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSrc.Play();
        
    }
    public void Run(){
        PlaySound("Running");
    }
    public void JumpSound(){
        PlaySound("Jumping");
    }
    public void Dive(){
 PlaySound("Diving");
    }    
    public void StopSound(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
      
        s.audioSrc.Stop();
    }
}
