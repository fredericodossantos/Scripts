using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    PathCreator creator;
    Path path;

    void OnSceneGUI()
    {
        Draw();
    }


    void Draw()
    {
        for (int i = 0; i < path.NumSegments; i++)
        {
            Vector2[] points = path.GetPointsInSegment(i);
            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2f);
        }
        
        Handles.color = Color.red;
        for (int j = 0; j < path.NumPoints; j++)
        {
            Vector2 newPos = Handles.FreeMoveHandle(path[j], Quaternion.identity, .1f, Vector2.zero, Handles.CylinderHandleCap);
            if (path[j] != newPos)
            {
                Undo.RecordObject(creator, "Move Point");
                path.MovePoint(j, newPos);
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
