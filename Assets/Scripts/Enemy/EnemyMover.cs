using System.Collections;
using Enemy;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float rotationDamping = 0.3f;
    [SerializeField] [Range(0,1)] private float movementDamping = 0.4f;
    [SerializeField] private AnimationCurve movementGraph;
    [SerializeField] private float moveSpeed = 5f;

    public Vector3 wanderCenterPosition;
    public float wanderRange;

    public bool isMovingToPlayer;

    [SerializeField] private bool _isDestinationSet;
    private Vector3 _targetDestination;
    private Vector3 _lastDir;
    
    private Rigidbody _rb;
    private EnemySensor _sensor;

    [SerializeField] private GameObject player;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _sensor = GetComponent<EnemySensor>();
    }

    private void FixedUpdate()
    {
        SetDestination();
        FindPath();
    }

    private void SetDestination()
    {
        if (isMovingToPlayer)
        {
            _isDestinationSet = true;
            _targetDestination = player.transform.position;
        }
        else if (!_isDestinationSet)
        {
            _isDestinationSet = true;
            var randomPos = Random.insideUnitSphere * wanderRange + wanderCenterPosition;
            _targetDestination = new Vector3(randomPos.x, transform.position.y, randomPos.z);
            Debug.Log(new Vector3(randomPos.x, transform.position.y, randomPos.z));
        }
    }
    
    private void FindPath()
    {
        MoveToTargetDestination();
    }
    
    private void MoveToTargetDestination()
    {
        if (Vector3.Distance(_targetDestination, transform.position) > 1f)
        {
            RotateTowardsTarget();
            _rb.velocity = (_targetDestination - transform.position + _lastDir * (movementDamping * 20)).normalized * 
                           (movementGraph.Evaluate(Vector3.Distance(transform.position, _targetDestination) / Mathf.Max(_sensor.sightRange, _sensor.sensoryRange)) * 
                            (moveSpeed *50* Time.fixedDeltaTime));
            _lastDir = _rb.velocity;
        }
        else StartCoroutine(SetIsDestinationSetFalseAfterDelay());
    }

    private void RotateTowardsTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_targetDestination - transform.position), Time.deltaTime/rotationDamping);
    }

    private IEnumerator SetIsDestinationSetFalseAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        _isDestinationSet = false;
        StopAllCoroutines();
    }

    private void LookAround()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,Random.Range(-50,50),0), Time.deltaTime/rotationDamping);
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawRay(transform.position, (player.transform.position - transform.position).normalized * 100);
    // }

}
