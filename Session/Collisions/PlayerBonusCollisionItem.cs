using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Player.PlayerCollision
{
    public abstract class PlayerBonusCollisionItem : PlayerBaseCollisionItem
    {
        [SerializeField]
        protected string bonusId;
        [SerializeField]
        protected float duration;

        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            Timer.Instance.RemoveTimer(x => x.id == bonusId);
            collision.gameObject.SetActive(false);
            Activate();
            var timerData = new TimerData()
            {
                id = bonusId,
                currentTime = 0,
                waitTime = duration,
                fullTime = 0,
                action = TimerAction
            };
            Timer.Instance.AddTimer(timerData);
            SessionController.Instance.BonusController.ActivateBonus(bonusId);
        }

        protected abstract void Activate();
        protected abstract void Deactivate();

        private void TimerAction()
        {
            Timer.Instance.RemoveTimer(x=>x.id == bonusId);
            Deactivate();
            SessionController.Instance.BonusController.DeactivateBonus(bonusId);
        }

    }
}