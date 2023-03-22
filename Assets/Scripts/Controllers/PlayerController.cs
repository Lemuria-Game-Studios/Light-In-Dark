using System.Collections;
using UnityEngine;
using Signals;

namespace Controllers
{
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed = 5;
        [SerializeField] private float turnSpeed = 360;
        [SerializeField] private float dashSpeed = 10;
        private bool _isDashing;
        private bool _isSpelling;
        private Animator _animator;
        private Vector3 _input;
        

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update() {
            GatherInput();
            Look();
            Controller();
        }
        private void FixedUpdate() {
            Move();
        }

        private void GatherInput() {
            
                _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        private void Look() {
            if (_input == Vector3.zero) return;

            var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        private void Move()
        {
            var transform1 = transform;
                rb.MovePosition(transform1.position + transform1.forward * (_input.normalized.magnitude * speed * Time.deltaTime));
                
                if (_input != Vector3.zero)
            {
                AnimationSignals.Instance.onMovementAnimation?.Invoke(_animator);
            }
            else if(_input==Vector3.zero)
            {
                AnimationSignals.Instance.onIdleAnimation?.Invoke(_animator);
            }
        }
        private void Controller()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayerSignals.Instance.onAttacking?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PlayerSignals.Instance.onSpelling?.Invoke();
            }

            if (Input.GetKeyDown((KeyCode.Space)))
            {
                //Dashing
            }
        }
    }

    

    public static class Helpers 
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }
}