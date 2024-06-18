using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpawnManagement
{
    public class SpawnManager:MonoBehaviour
    {
        public List<SpawnPoint> spawnPoints;

        public static SpawnManager instance;

        [Networked]
        public PlayerRef spawnPlayer { get; set; }
        // public bool 
        // Start is called before the first frame update
        void Awake()
        {
            if(instance== null)
            {
                instance = this;
            }
            else if(instance!=this)
            {
                Destroy(gameObject);
                return;
            }
            InitSpawns();
            DontDestroyOnLoad(gameObject);
        }


        private void InitSpawns()
        {
            spawnPoints = new List<SpawnPoint>();
            SpawnPoint[] spawns = FindObjectsOfType<SpawnPoint>();

            foreach (SpawnPoint spa in spawns)
            {
                spawnPoints.Add(spa);
            }
        }

        public void SpawnPlayers()
        {
            InitSpawns();
            List<string> spawns=GetSpawns(PlayerManagement.instance.playerList.Count);
            if(spawns!=null)
            {
                for(int i=0; i<spawns.Count; i++)
                {
                    SpawnPlayer(PlayerManagement.instance.playerList[i], spawns[i]);
                }
            }
        }

        public List<string> GetSpawns(int numOfPlayers)
        {
            if (spawnPoints.Count >= numOfPlayers)
            {
                int i = 0;
                List<string> names = new List<string>();

                while (spawnPoints.Count - 1 > i) //iterate until random spawn points have been found
                {
                    SpawnPoint c = spawnPoints[Random.Range(0, spawnPoints.Count - 1)]; //inefficent brute force

                    if (c.inUse == false)
                    {
                        i++;
                        c.inUse = true;
                        names.Add(c.name); //iterate and add to list, whilst excluding this from future
                    }


                }

                return names;

            }

            //Return empty list if there are not enough spawn points
            return new List<string>();
        }

        public void SpawnPlayer(PlayerDetails p, string Spawnname)
        {
            foreach (SpawnPoint spa in spawnPoints)
            {
                if (spa.name == Spawnname)
                {
                    p.MoveAndLookAtRPC(spa.transform.position, spa.transform.forward);
                    
                }
            }
        }

    }
}
