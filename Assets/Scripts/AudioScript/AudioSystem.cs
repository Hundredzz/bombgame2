using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;
    public string[] name;
    public AudioSource source;
    public Dictionary<string, AudioClip> Clip = new();

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Clip.Clear();
        for(int i = 0;i < clip.Length; i++)
        {
            Clip[name[i]] = clip[i];
        }
    }

    public void PlayAudio(string name)
    {
        source.PlayOneShot(Clip[name]);
    }
}
