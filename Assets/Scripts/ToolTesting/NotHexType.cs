using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NotHexType : ScriptableObject
{
    [Range(1f, 8f)]
    public float radius = 5;
    public float damage = 4;
    public Color color = Color.red;

}
