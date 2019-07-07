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

        [SerializeField]
        private MovementObjectsController movementObjectsController;
        public MovementObjectsController MovementObjectsController
        {
            get { return movementObjectsController; }
        }

        public override void Init()
        {
            base.Init();
            movementObjectsController.OnMovementObjectPassed += OnBarrierPassed;
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

        public void OnBarrierPassed(MovementObject movementObject)
        {
            if (movementObject.gameObject.CompareTag(GameConstants.BARRIER_TAG))
            {
                Scores++;
            }
        }

    }
}