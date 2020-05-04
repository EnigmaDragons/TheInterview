using UnityEngine;

    public abstract class GameEventTrigger : MonoBehaviour
    {
        protected abstract GameEvent Trigger { get; }

        private void Awake() => Execute();

        private void OnEnable() => Trigger.Subscribe(Execute, this);
        private void OnDisable() => Trigger.Unsubscribe(this);

        protected abstract void Execute();        
    }
