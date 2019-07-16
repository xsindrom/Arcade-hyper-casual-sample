using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Session.Player
{
    using PlayerCollision;

    public class PlayerController : MonoBehaviour
    {
        public PlayerCollisionSettings CollisionSettings { get; set; }
        [SerializeField]
        private MeshFilter meshFilter;
        public MeshFilter MeshFilter
        {
            get { return meshFilter; }
        }
        [SerializeField]
        private new MeshRenderer renderer;
        public MeshRenderer Renderer
        {
            get { return renderer; }
        }
        [SerializeField]
        private PlayerMovementDataProxy movementProxy;
        public PlayerMovementDataProxy MovementProxy
        {
            get { return movementProxy; }
        }
        [SerializeField]
        private new Collider collider;
        public Collider Collider
        {
            get { return collider; }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!CollisionSettings)
                return;

            var collisionItem = CollisionSettings.CollisionItems.Find(x => x.CollisionTag == collision.gameObject.tag);
            collisionItem?.OnCollision(collision);
        }
    }
}