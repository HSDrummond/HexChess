using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NotHexManager : MonoBehaviour
{
    public static List<NotHex> allNotHexes = new List<NotHex>();


    public static void UpdateAllNotHexColors()
    {
        foreach (NotHex nothex in allNotHexes)
        {
            nothex.TryApplyColor();
        }
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (NotHex nothex in allNotHexes)
        {
            if (nothex.type == null)
                continue;

            Vector3 managerPos = transform.position;
            Vector3 notHexPos = nothex.transform.position;
            float halfHeight = (managerPos.y - notHexPos.y) * .5f;
            Vector3 offset = Vector3.up * halfHeight;

            Handles.DrawBezier(
                managerPos,
                notHexPos,
                managerPos - offset,
                notHexPos + offset,
                nothex.type.color,
                EditorGUIUtility.whiteTexture,
                1f
            );

            //Handles.DrawAAPolyLine( transform.position, nothex.transform.position );
        }
    }
    #endif
}
