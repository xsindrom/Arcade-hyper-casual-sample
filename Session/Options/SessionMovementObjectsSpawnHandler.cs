using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    public abstract class SessionMovementObjectsSpawnHandler : ScriptableObject
    {
        [SerializeField]
        protected int spawnMaxCount;
        [SerializeField]
        protected float spawnMinInterval;
        [SerializeField]
        protected float spawnMaxInterval;
        [SerializeField]
        protected List<string> filter = new List<string>();

        [NonSerialized]
        protected int spawnedCount;
        [NonSerialized]
        protected float spawnInterval;
        [NonSerialized]
        protected float spawnPrevInterval;
        [NonSerialized]
        protected List<MovementObject> spawnedObjects = new List<MovementObject>();

        public int SpawnMaxCount
        {
            get { return spawnMaxCount; }
        }

        public float SpawnMinInterval
        {
            get { return spawnMinInterval; }
        }

        public float SpawnMaxInterval
        {
            get { return spawnMaxInterval; }
        }

        public List<string> Filter
        {
            get { return filter; }
        }

        public int SpawnedCount
        {
            get { return spawnedCount; }
            set { spawnedCount = value; }
        }

        public float SpawnInterval
        {
            get { return spawnInterval; }
            set { spawnInterval = value; }
        }

        public float SpawnPrevInterval
        {
            get { return spawnPrevInterval; }
            set{ spawnPrevInterval = value; }
        }

        public List<MovementObject> SpawnedObjects
        {
            get { return spawnedObjects; }
        }

        public abstract string GenerateId();
    }
}