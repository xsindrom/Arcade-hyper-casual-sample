using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session
{
    public class BonusController : MonoBehaviour
    {
        [SerializeField]
        private List<string> activeBonuses = new List<string>();
        public List<string> ActiveBonuses
        {
            get { return activeBonuses; }
        }

        public event Action<string> OnBonusActivated;
        public event Action<string> OnBonusDeactivated;

        public void ActivateBonus(string id)
        {
            if (activeBonuses.Contains(id))
                return;

            activeBonuses.Add(id);
            OnBonusActivated?.Invoke(id);
        }

        public void DeactivateBonus(string id)
        {
            activeBonuses.Remove(id);
            OnBonusDeactivated?.Invoke(id);
        }
    }
}