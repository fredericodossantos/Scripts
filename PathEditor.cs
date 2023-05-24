using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : MonoBehaviour
{
    PathCreator creator;
    Path path;

    void OnSceneGUI()
    {
        Draw();
    }


    void Draw()
    {
        for (int i = 0; i < path.points.Count; i++)
        {
           Vector2 newPos = Handles.FreeMoveHandle(path.points[i], Quaternion.identity, .1f, Vector2.zero, Handles.CubeHandleCap);
           if (newPos != path.points[i])
           {
               Undo.RecordObject(creator, "Move Point");
               path.points[i] = newPos;
           }
        }
    }

    void OnEnable()
    {
        creator = (PathCreator)target;
        if (creator.path == null)
        {
            creator.CreatePath();
        }
        path = creator.path;
    }
    

}
