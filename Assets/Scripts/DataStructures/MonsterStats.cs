using System;
using UnityEngine;

namespace DataStructures
{
    [Serializable]
    public class MonsterStats
    {
        public Pair<int, int> attack;
        public Pair<int, int> defense;
    }
}