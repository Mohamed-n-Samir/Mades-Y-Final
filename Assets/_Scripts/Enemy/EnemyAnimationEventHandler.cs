using System;
using UnityEngine;

    public class EnemyAnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action OnAttack;
        public event Action OnDie;

        private void AnimationFinished() => OnFinish?.Invoke();
        private void Attack() => OnAttack?.Invoke(); 
        private void Dying() => OnDie?.Invoke(); 
    
}