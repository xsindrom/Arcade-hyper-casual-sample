using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    [Serializable]
    public class SessionsData
    {
        [SerializeField]
        private int highScore;
        [SerializeField]
        private List<string> availableSessions = new List<string>();
        [SerializeField]
        private List<string> completedSessions = new List<string>();

        public int HighScore
        {
            get { return highScore; }
            set { highScore = value; }
        }

        public List<string> AvailableSessions
        {
            get { return availableSessions; }
        }

        public List<string> CompletedSessions
        {
            get { return completedSessions; }
        }
    }
}