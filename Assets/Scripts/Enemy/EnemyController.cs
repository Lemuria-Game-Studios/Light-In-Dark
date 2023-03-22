using Enemy;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemySensor _sensor;
    private EnemyMover _mover;

    private void Awake()
    {
        _sensor = GetComponent<EnemySensor>();
        _mover = GetComponent<EnemyMover>();
    }

    private void Update()
    {
        CheckIfPlayerDetected();
    }

    private void CheckIfPlayerDetected()
    {
        _mover.isMovingToPlayer = _sensor.isPlayerDetected;
    }
}
