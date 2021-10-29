using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Clips
{
    Hit,
    Attack,
    Jump
}

public class SoundEffect : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;

    private Clips clips;

    public AudioClip GetClip(Clips clip)
    {
        return _clips[(int)clip];
    }
}
