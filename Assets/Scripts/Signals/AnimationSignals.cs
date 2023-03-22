using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AnimationSignals : MonoSingleton<AnimationSignals>
    {
        public UnityAction<Animator> onIdleAnimation =delegate {  };
        public UnityAction<Animator> onMovementAnimation =delegate {  };
        public UnityAction<Animator> onAttackingAnimation =delegate {  };
        public UnityAction<Animator> onSpellingAnimation =delegate {  };
    }
}
