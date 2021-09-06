using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [Range(1, 7)] 
    public float walkSpeed;
    [Range(300, 800)] 
    public float turnSpeed;
    [Range(10, 40)] 
    public float dragSpeed;
    [Range(1, 3)] 
    public float dragCoolDown;
    [Range(8, 15)] 
    public float throwDistance;
    [Range(0.5f, 2f)] 
    public float throwWobble;
    [Range(10, 30)] 
    public float sightRadius;
    [Range(120, 360)] 
    public float sightAngle;
}
