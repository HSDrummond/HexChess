using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SnapperTool : EditorWindow
{

    [MenuItem("Epic Custom Tools/Snapper")]
    public static void OpenSnapperTool() => GetWindow<SnapperTool>();

}
