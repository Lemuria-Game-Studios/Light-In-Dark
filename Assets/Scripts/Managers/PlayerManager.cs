using System;
using UnityEngine;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private Animator _animator;
        private void OnEnable()
        {
            SubscribeEvents();
            _animator = GetComponent<Animator>();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onAttacking += OnAttacking;
            PlayerSignals.Instance.onSpelling += OnSpelling;
        }

        private void OnAttacking()
        {
            AnimationSignals.Instance.onAttackingAnimation?.Invoke(_animator);
        }

        private void OnSpelling()
        {
            AnimationSignals.Instance.onSpellingAnimation?.Invoke(_animator);
        }
        
    }
}
