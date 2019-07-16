using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Player.PlayerCollision
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Session/PlayerCollision/PlayerShieldBonusCollisionItem")]
    public class PlayerShieldBonusCollisionItem : PlayerBonusCollisionItem
    {
        protected override void Activate()
        {
            SessionController.Instance.Player.Collider.enabled = false;
        }

        protected override void Deactivate()
        {
            SessionController.Instance.Player.Collider.enabled = true;
        }
    }
}