using System;
using UnityEngine;
using Signals;

namespace Managers
{
    public class AnimationManagers : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AnimationSignals.Instance.onIdleAnimation += OnIdleAnimation;
            AnimationSignals.Instance.onMovementAnimation += OnMovementAnimation;
            AnimationSignals.Instance.onAttackingAnimation += OnAttackingAnimation;
            AnimationSignals.Instance.onSpellingAnimation += OnSpellingAnimation;
        }
        private void OnIdleAnimation(Animator animator)
        {
            animator.SetFloat("Blend",0f);
        }
        private void OnAttackingAnimation(Animator animator)
        {
            animator.SetTrigger("Attack");
        }

        private void OnMovementAnimation(Animator animator)
        {
            animator.SetFloat("Blend",1f);
        }
        private void OnSpellingAnimation(Animator animator)
        {
            animator.SetTrigger("Spell");
        }
       
    }
}
