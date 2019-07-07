using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/Options/SessionPlayerMovementOption")]
    public class SessionPlayerMovementOption : SessionOption
    {
        [SerializeField]
        private float movementRadius;
        [SerializeField]
        private Vector2 movementMultipler;

        public float MovementRadius
        {
            get { return movementRadius; }
        }

        public Vector2 MovementMultipler
        {
            get { return movementMultipler; }
        }

        public override void StartSession()
        {
            SessionController.Instance.Player.MovementProxy.Value = new PlayerMovementData()
            {
                radius = movementRadius,
                xMultipler = movementMultipler.x,
                yMultipler = movementMultipler.y
            };
        }
    }
}