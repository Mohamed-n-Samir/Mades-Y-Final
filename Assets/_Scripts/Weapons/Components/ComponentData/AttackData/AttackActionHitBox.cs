using System;
using UnityEngine;

    [Serializable]
    public class AttackActionHitBox : AttackData
    {
        public bool Debug;
        [field: SerializeField] public Rect HitBox { get;private set; }

        // public void SwapRectSize(){

        //     HitBox = new Rect(HitBox.x , HitBox.y ,HitBox.height,HitBox.width);
        // }
    }