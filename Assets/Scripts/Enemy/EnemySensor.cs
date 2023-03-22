using System.Collections;
using UnityEngine;

namespace Enemy
{
     public class EnemySensor : MonoBehaviour
     {
          [SerializeField] public float sightRange;
          [SerializeField] [Range(0,360)] public float sightAngle;
          [SerializeField] public float sensoryRange;
          [SerializeField] private LayerMask targetLayer;
          [SerializeField] private LayerMask obstructionLayer;
     
          public bool isPlayerDetected;


          public GameObject player;

          private void Awake()
          {
               //find player here
          }

          private void Start()
          {
               StartCoroutine(FOVRoutine());
          }

          private IEnumerator FOVRoutine()
          {
               WaitForSeconds wait = new WaitForSeconds(0.2f);
          
               while (true)
               {
                    yield return wait;
                    CheckFOV();
               }
          }

          private void CheckFOV()
          {
               Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Mathf.Max(sightRange, sensoryRange), targetLayer);

               if (rangeChecks.Length != 0)
               {
                    Transform target = rangeChecks[0].transform;
                    Vector3 directionToTarget = (target.position - transform.position).normalized;
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
               
                    //sensoryFOV
                    if (distanceToTarget <= sensoryRange)
                    {
                         //none obstruction if it senses
                         isPlayerDetected = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, 0);
                    }
                    //sightFOV
                    else if (Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2 && distanceToTarget <= sightRange)
                    {
                         //obstruction exists even it sees
                         isPlayerDetected = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer);
                    }
                    else isPlayerDetected = false;
               }
               else if (isPlayerDetected)
                    isPlayerDetected = false;
          }
     }
}
