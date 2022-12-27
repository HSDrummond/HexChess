using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;

public class SnapperTool : EditorWindow
{

    [MenuItem("Epic Custom Tools/Snapper")]
    public static void OpenSnapperTool() => GetWindow<SnapperTool>("Snapper");

    private void OnEnable()
    {
        Selection.selectionChanged += Repaint;
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= DuringSceneGUI;
    }

    enum SnapViewPrefs
    {
        Nothing,
        Crosses,
        Paths
    }

    SnapViewPrefs currViewPref;
    int gridValues = 1;

    void DuringSceneGUI (SceneView sceneView )
    {
        if (currViewPref == SnapViewPrefs.Crosses)
        {
            DisplayAllSnapCrosses();
        }
        else if (currViewPref == SnapViewPrefs.Paths)
        {
            DisplayAllSnapPaths();
        }
    }


    public void OnGUI()
    {
        GUILayout.Label("epic snapping button");

        using (new EditorGUI.DisabledGroupScope(Selection.gameObjects.Length == 0))
        {
            if (GUILayout.Button("Snap Selected Objects"))
            {
                SnapSelection();
            }
        }

        currViewPref = (SnapViewPrefs)EditorGUILayout.EnumPopup("Snap Preview Options", currViewPref);
        gridValues = EditorGUILayout.IntField("Grid Size", gridValues);
        if (gridValues <= 0)
        {
            gridValues = 1;
        }

    }

    void SnapSelection()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Undo.RecordObject(go.transform, "snapped objects");
            go.transform.position = go.transform.position.Round(gridValues);
        }
    }

    void DisplayAllSnapPaths()
    {
        foreach (NotHex go in NotHexManager.allNotHexes)
        {
            Vector3 snappedPos = go.transform.position.Round(gridValues);
            Handles.DrawLine(go.transform.position, snappedPos);
        }
    }

    void DisplayAllSnapCrosses()
    {
        foreach (NotHex go in NotHexManager.allNotHexes)
        {
            Vector3 snappedPos = go.transform.position.Round(gridValues);
            Handles.DrawLine(snappedPos, snappedPos + Vector3.up);
            Handles.DrawLine(snappedPos, snappedPos + Vector3.down);
            Handles.DrawLine(snappedPos, snappedPos + Vector3.right);
            Handles.DrawLine(snappedPos, snappedPos + Vector3.left);
            Handles.DrawLine(snappedPos, snappedPos + Vector3.forward);
            Handles.DrawLine(snappedPos, snappedPos + Vector3.back);
        }
    }
}
