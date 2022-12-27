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

    public static Vector3 Round( this Vector3 v , int i = 1)
    {
        v.x = RoundByInt(v.x, i);
        v.y = RoundByInt(v.y, i);
        v.z = RoundByInt(v.z, i);
        return v;
    }

    public static int RoundByInt(float a, int i)
    {
        i = Mathf.Abs(i);
        int min = ((int)(a / i)) * i;
        int max = min + i;

        float minDiff = a - min;
        float maxDiff = max - a;

        if (minDiff == maxDiff)
        {
            if (minDiff % 2 == 0)
            {
                return min;
            }
            return max;
        }
        else if (minDiff < maxDiff)
        {
            return min;
        }
        return max;
    }

}
