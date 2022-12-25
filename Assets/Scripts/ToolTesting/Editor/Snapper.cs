using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public static class Snapper
{

    const string UNDO_SNAP_NAME = "snapped objects";

    [MenuItem("Epic Custom Tools/Snap Selected Objects %&s", isValidateFunction: true)]
    public static bool SnapTheThingsValidate()
    {
        return Selection.gameObjects.Length > 0;
    }


    [MenuItem("Epic Custom Tools/Snap Selected Objects %&s")]
    public static void SnapTheThings()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Undo.RecordObject(go.transform, UNDO_SNAP_NAME);
            go.transform.position = go.transform.position.Round();
        }


    }

    public static Vector3 Round( this Vector3 v )
    {
        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);
        v.z = Mathf.Round(v.z);
        return v;
    }


}
