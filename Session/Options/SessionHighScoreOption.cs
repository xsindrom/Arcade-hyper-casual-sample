using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Session/Options/SessionHighScoreOption")]
    public class SessionHighScoreOption : SessionOption
    {

        public override void LoseSession()
        {
            var scores = SessionController.Instance.Scores;
            var sessionData = GameController.Instance.StorageController.StorageData.SessionsData;
            if (sessionData.HighScore < scores)
            {
                sessionData.HighScore = scores;
            }
        }
    }
}