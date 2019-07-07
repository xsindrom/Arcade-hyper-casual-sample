using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Player.PlayerCollision
{
    public abstract class PlayerBaseCollisionItem : ScriptableObject
    {
        [SerializeField]
        protected string collisionTag;

        public string CollisionTag
        {
            get { return collisionTag; }
        }

        public virtual void OnCollision(Collision collision)
        {
            Debug.Log($"Collided with tag: {collisionTag}");
        }
    }
}