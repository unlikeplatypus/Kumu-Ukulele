using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "ScriptableObjects/Song", order = 1)]
public class Song : ScriptableObject
{
    [SerializeField] private string _name = "Defualt Song Name";
    [SerializeField] private float _length = 0.0f;
    [SerializeField] private AudioClip _musicAudio = null;
    [SerializeField] private NoteType[] notes = null;

    public string Name { get => _name; set => _name = value; }
    public float Length { get => _length; set => _length = value; }
    public AudioClip MusicAudio { get => _musicAudio; set => _musicAudio = value; }
    public NoteType[] Notes { get => notes; set => notes = value; }
}
