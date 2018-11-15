using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    public static List<AudioClipPlayer> pool = new List<AudioClipPlayer>();

    public static int[] steps = { 0, 2, 4, 5, 7, 9, 11, 12 };

    public static float GetRandomPitch()
    {
        return Mathf.Pow(1.05946f, steps[Random.Range(0, 7)]);
    }

    public static void Play(AudioClip clip, float pitch, float volume, GameObject fallback)
    {

        if (pool.Count > 0)
        {
            pool[pool.Count - 1].Initialize(clip, pitch, volume, Vector2.zero, 0);
        }
        else
        {
            Instantiate(fallback).GetComponent<AudioClipPlayer>().Initialize(clip, pitch, volume, Vector2.zero, 0);
        }

    }

    public static void Play(AudioClip clip, float pitch, float volume, Vector2 position, GameObject fallback)
    {

        if (pool.Count > 0)
        {
            pool[pool.Count - 1].Initialize(clip, pitch, volume, position, 1);
        }
        else
        {
            Instantiate(fallback).GetComponent<AudioClipPlayer>().Initialize(clip, pitch, volume, position, 1);
        }

    }

    AudioSource audioSource;
    new Transform transform;

    public void Initialize(AudioClip clip, float pitch, float volume, Vector2 position, float spatialBlend)
    {

        pool.Remove(this);

        DontDestroyOnLoad(gameObject);

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.spatialBlend = spatialBlend;
        audioSource.Play();

        if (transform == null) transform = GetComponent<Transform>();
        transform.position = position;

        StartCoroutine(PoolWhenEnd());

    }

    IEnumerator PoolWhenEnd()
    {

        while (audioSource.isPlaying) yield return null;
        pool.Add(this);

    }

}
