using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pattern", menuName = "ScriptableObjects/Pattern", order = 0)]
public class EnemyPattern : ScriptableObject
{
    [SerializeField] private Obstacle[] _pattern = null;

    public Obstacle[] Pattern { get => _pattern; set => _pattern = value; }
}
