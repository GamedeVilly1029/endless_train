using System;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public Sound[] Sounds;
    [SerializeField] private AudioDataTransfer _transfer;

    private void Awake()
    {
        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.loop = sound.Loop;
            sound.Source.volume = sound.Volume;
        }
    }

    private void Start()
    {
        if (_transfer.ToggleAudio)
        {
            PlaySound("ambience");
        }
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(Sounds, sound => sound.Name == name);
        sound.Source.Play();
    }
}