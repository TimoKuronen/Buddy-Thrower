using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public float walkSpeed;
    public float turnSpeed;
    public float dragSpeed;
    public float dragCoolDown;
    public float throwDistance;
    public float throwWobble;
    public float sightRadius;
    public float sightAngle;
}
