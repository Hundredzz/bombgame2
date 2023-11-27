using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;
    public string[] name;
    public static AudioSource source;
    public static Dictionary<string, AudioClip> Clip = new();

    private void Start()
    {
        Clip.Clear();
        for(int i = 0;i < clip.Length; i++)
        {
            Clip[name[i]] = clip[i];
        }
    }

    public static void PlayAudio(string name)
    {
        source.PlayOneShot(Clip[name]);
    }
}
