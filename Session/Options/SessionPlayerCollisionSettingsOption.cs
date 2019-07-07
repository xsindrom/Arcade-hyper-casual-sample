using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    using Player.PlayerCollision;

    [CreateAssetMenu(menuName ="ScriptableObjects/Session/Options/SessionPlayerCollisionSettingsOption")]
    public class SessionPlayerCollisionSettingsOption : SessionOption
    {
        [SerializeField]
        private PlayerCollisionSettings playerCollisionSettings;
        public PlayerCollisionSettings PlayerCollisionSettings
        {
            get { return playerCollisionSettings; }
        }

        public override void StartSession()
        {
            SessionController.Instance.Player.CollisionSettings = playerCollisionSettings;
        }
    }
}