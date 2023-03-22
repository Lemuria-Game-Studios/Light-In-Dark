using UnityEditor;
using UnityEngine;

namespace Enemy
{
    [CustomEditor(typeof(EnemySensor))]
    public class EnemySensorEditor : Editor
    {
        private EnemySensor _sensor;
        private void OnSceneGUI()
        {
            _sensor = (EnemySensor)target;
            SetSightFOV();
            SetSensoryFOV();
            SetPlayerDetection();
        }

        private void SetPlayerDetection()
        {
            if (_sensor.isPlayerDetected)
            {
                Handles.color = Color.black;
                Handles.DrawLine(_sensor.transform.position, _sensor.player.transform.position);
            }
        }

        private void SetSensoryFOV()
        {
            Handles.color = Color.yellow;
            Handles.DrawWireArc(_sensor.transform.position, Vector3.up, Vector3.forward,  360f, _sensor.sensoryRange);
        }

        private void SetSightFOV()
        {
            Handles.color = Color.red;
            var pos = _sensor.transform.position;
            Handles.DrawWireArc(pos, Vector3.up, DirectionFromAngle(_sensor.transform.eulerAngles.y, -_sensor.sightAngle / 2) , _sensor.sightAngle, _sensor.sightRange);
            Vector3 viewAngle01 = DirectionFromAngle(_sensor.transform.eulerAngles.y, -_sensor.sightAngle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(_sensor.transform.eulerAngles.y, _sensor.sightAngle / 2);
        
            Handles.DrawLine(pos, pos + viewAngle01 * _sensor.sightRange);
            Handles.DrawLine(pos, pos + viewAngle02 * _sensor.sightRange);
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
        
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
