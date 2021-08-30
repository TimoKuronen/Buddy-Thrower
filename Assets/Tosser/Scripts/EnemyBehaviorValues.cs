using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviorValues", menuName = "ScriptableObjects/BehaviorValues")]
public class EnemyBehaviorValues : ScriptableObject
{
    public float tossInterval; // how often we drag and throw buddy
    public float tossAccuracy; // how many meters will we throw target "off" ?
}
