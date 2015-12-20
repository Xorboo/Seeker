using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip HitS;
    public AudioClip YellowHitS;
    public AudioClip CollectS;
    public AudioClip ThrowS;

    public AudioClip BG;

    void Start()
    {
        SoundKit.Instance.PlayBackgroundMusic(BG, 1f);
    }

    public void Poison()
    {
        SoundKit.Instance.PlayOneShot(YellowHitS);
    }

    public void Hit()
    {
        SoundKit.Instance.PlayOneShot(HitS);
    }

    public void Collect()
    {
        SoundKit.Instance.PlayOneShot(CollectS);
    }

    public void Throw()
    {
        SoundKit.Instance.PlayOneShot(ThrowS);
    }
}