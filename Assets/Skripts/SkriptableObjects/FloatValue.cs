using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObjects doenst exist in Scenes
// instead they live parallel and so can be used in multiple scenes (doenst get reset)

// allows to create as Object with rightclick
[CreateAssetMenu]
public class FloatValue : ScriptableObject
{
    public float initialValue;
}
