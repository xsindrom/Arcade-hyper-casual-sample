using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Player.PlayerCollision
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/PlayerCollision/PlayerScoreBonusCollisionItem")]
    public class PlayerScoreBonusCollisionItem : PlayerBonusCollisionItem
    {
        [SerializeField]
        private int scoreMultipler;
        private int prevScoreMultipler;

        protected override void Activate()
        {
            prevScoreMultipler = SessionController.Instance.ScoresMultipler;
            SessionController.Instance.ScoresMultipler = scoreMultipler;
        }

        protected override void Deactivate()
        {
            SessionController.Instance.ScoresMultipler = prevScoreMultipler;
        }
    }
}