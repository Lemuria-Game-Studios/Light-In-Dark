using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction onAttacking = delegate { };
        public UnityAction onSpelling = delegate {  };
    }
}
