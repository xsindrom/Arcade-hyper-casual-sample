using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;
namespace Session.Player.PlayerCollision
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/PlayerCollision/PlayerCurrencyCollisionItem")]
    public class PlayerCurrencyCollisionItem : PlayerBaseCollisionItem
    {
        [SerializeField]
        private CurrencyItem currency;

        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            GameController.Instance.CurrencyController.AddCurrency(currency.currencyType, currency.currencyAmount);
            collision.gameObject.SetActive(false);
        }
    }
}