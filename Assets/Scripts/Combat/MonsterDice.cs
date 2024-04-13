using System;
using System.Collections.Generic;
using DataStructures;
using Dice;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat
{
    [Serializable]
    public class MonsterDice
    {
        [HideInInspector] public string monsterName;
        
        [HideInInspector] public int attack;
        [HideInInspector] public int defense;
        
        private readonly List<Pair<MonsterCrests, Sprite>> _faces;

        public MonsterDice(string monsterName, int attack, int defense, List<Pair<MonsterCrests, Sprite>> faces)
        {
            this.monsterName = monsterName;
            this.attack = attack;
            this.defense = defense;
            _faces = faces;
        }
        
        public Pair<MonsterCrests, Sprite> GetRandomFace()
        {
            return _faces[Random.Range(0, _faces.Count)];
        }
    }
}
