using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Player.PlayerCollision
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/PlayerCollision/PlayerBarrierCollisionItem")]
    public class PlayerBarrierCollisionItem : PlayerBaseCollisionItem
    {
        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            SessionController.Instance.LoseSession();
        }
    }
}