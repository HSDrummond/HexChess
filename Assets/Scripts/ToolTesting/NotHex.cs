using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif



[ExecuteAlways] // be careful with update functions in this class because of this
public class NotHex : MonoBehaviour
{

    public NotHexType type;

    static readonly int shPropColor = Shader.PropertyToID( "_BaseColor" );

    MaterialPropertyBlock mpb;
    MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }

    private void OnValidate()
    {
        TryApplyColor();
    }

    public void TryApplyColor()
    {
        if (type == null)
            return;

        MeshRenderer rnd = GetComponent<MeshRenderer>();
        Mpb.SetColor(shPropColor, type.color);
        rnd.SetPropertyBlock(Mpb);
    }

    void OnEnable()
    {
        TryApplyColor();
        NotHexManager.allNotHexes.Add(this);
    }

    void OnDisable() => NotHexManager.allNotHexes.Remove(this);



    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (type == null)
            return;

        Handles.color = type.color;
        Handles.DrawWireDisc(transform.position, transform.up, type.radius);
        Handles.color = Color.white;
    }
    #endif

}
