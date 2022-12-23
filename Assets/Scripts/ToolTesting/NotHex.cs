using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways] // be careful with update functions in this class because of this
public class NotHex : MonoBehaviour
{
    [Range(1f, 8f)]
    public float radius = 5;

    public float damage = 4;
    public Color color = Color.red;

    void OnEnable() => NotHexManager.allNotHexes.Add(this);
    void OnDisable() => NotHexManager.allNotHexes.Remove(this);



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere( transform.position, radius );
    }

}
