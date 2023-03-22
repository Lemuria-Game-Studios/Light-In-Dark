using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyMover))]
public class EnemyMoverEditor : Editor
{
    private EnemyMover _mover;
    
    private void OnSceneGUI()
    {
        _mover = (EnemyMover)target;
        SetWanderField();
    }

    private void SetWanderField()
    {
        Handles.color = Color.green;
        Handles.DrawWireArc(_mover.wanderCenterPosition, Vector3.up, Vector3.forward,  360f, _mover.wanderRange);
    }
}
