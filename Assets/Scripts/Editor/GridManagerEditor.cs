using UnityEngine;
using UnityEditor;
using AStar;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    private Node selectedNode;

    private void OnSceneGUI()
    {
        GridManager gridManager = (GridManager)target;

        // Detect mouse clicks
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            // Raycast to detect node
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridManager.GetLayerMask()))
            {
                Node node = hit.collider.GetComponentInParent<Node>();
                if (node != null)
                {
                    selectedNode = node;
                    e.Use();

                    // Mark the editor as dirty to refresh the properties
                    GUI.changed = true;
                    EditorUtility.SetDirty(target);
                }
            }
        }

        // Draw handles for the selected node
        if (selectedNode != null)
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(selectedNode.transform.position, Vector3.up, 1f);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (selectedNode != null)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Selected Node", EditorStyles.boldLabel);
            selectedNode.NodeData.Cost = EditorGUILayout.FloatField("Cost", selectedNode.NodeData.Cost);
            selectedNode.NodeData.IsWalkable = EditorGUILayout.Toggle("Is Walkable", selectedNode.NodeData.IsWalkable);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(selectedNode);
                EditorUtility.SetDirty(target);
            }
        }
    }
}
