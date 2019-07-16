using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Session
{
    using Player;
    using Options;

    public interface ISessionHandler : IEventHandler
    {
        void StartSession();
        void WinSession();
        void LoseSession();
        void ContinueSession();
    }

    public class SessionController : MonoSingleton<SessionController>
    {
        [SerializeField]
        private PlayerController player;
        public PlayerController Player
        {
            get { return player; }
        }

        private SessionItemsStorage sessionItemsStorage;
        public SessionItemsStorage SessionItemsStorage
        {
            get
            {
                if (!sessionItemsStorage)
                {
                    sessionItemsStorage = ResourceManager.GetResource<SessionItemsStorage>(GameConstants.PATH_SESSION_ITEMS_STORAGE);
                }
                return sessionItemsStorage;
            }
        }
        public SessionItemsStorage.SessionData CurrentSessionData { get; set; }

        private int scores;
        public int Scores
        {
            get { return scores; }
            set
            {
                if (scores != value)
                {
                    var prevScores = scores;
                    scores = value;
                    OnScoresChanged?.Invoke(prevScores, scores);
                }
            }
        }
        public event Action<int,int> OnScoresChanged;

        private int scoresMultipler;
        public int ScoresMultipler
        {
            get { return scoresMultipler; }
            set { scoresMultipler = value; }
        }

        [SerializeField]
        private MovementObjectsController movementObjectsController;
        public MovementObjectsController MovementObjectsController
        {
            get { return movementObjectsController; }
        }

        [SerializeField]
        private BonusController bonusController;
        public BonusController BonusController
        {
            get { return bonusController; }
        }

        public override void Init()
        {
            base.Init();
            movementObjectsController.OnMovementObjectPassed += OnBarrierPassed;
        }

        public void OnBarrierPassed(MovementObject movementObject)
        {
            if (movementObject.gameObject.CompareTag(GameConstants.BARRIER_TAG))
            {
                Scores += ScoresMultipler;
            }
        }

        public void StartSession()
        {
            var defaultSessionData = SessionItemsStorage.Sessions.Find(x => x.Id == GameConstants.DEFAULT_SESSION_ID);
            if (defaultSessionData == null)
                return;

            StartSession(defaultSessionData);
        }

        public void StartSession(SessionItemsStorage.SessionData sessionData)
        {
            if (sessionData == null)
                return;

            Scores = 0;
            ScoresMultipler = 1;

            CurrentSessionData = sessionData;
            CurrentSessionData.SessionItem.StartSession();
            EventManager.Call<ISessionHandler>(x => x.StartSession());
        }

        public void WinSession()
        {
            movementObjectsController.Stop();
            CurrentSessionData.SessionItem.WinSession();
            EventManager.Call<ISessionHandler>(x => x.WinSession());
        }

        public void LoseSession()
        {
            movementObjectsController.Stop();
            CurrentSessionData.SessionItem.LoseSession();
            EventManager.Call<ISessionHandler>(x => x.LoseSession());
        }

        public void ContinueSession()
        {
            CurrentSessionData.SessionItem.StartSession();
            EventManager.Call<ISessionHandler>(x => x.StartSession());
            movementObjectsController.Continue();
            EventManager.Call<ISessionHandler>(x => x.ContinueSession());
        }
    }
}