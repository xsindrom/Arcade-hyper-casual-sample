using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Player.PlayerCollision
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/PlayerCollision/Settings", order = 0)]
    public class PlayerCollisionSettings : ScriptableObject
    {
        [SerializeField]
        private List<PlayerBaseCollisionItem> collisionItems = new List<PlayerBaseCollisionItem>();
        public List<PlayerBaseCollisionItem> CollisionItems
        {
            get { return collisionItems; }
        }
    }
}