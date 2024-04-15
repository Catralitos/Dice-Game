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
        
        public readonly List<Pair<MonsterCrests, Sprite>> Faces;

        private bool _summoned;
        
        public MonsterDice(string monsterName, List<Pair<MonsterCrests, Sprite>> faces)
        {
            this.monsterName = monsterName;
            Faces = faces;
        }

        public void Summon(int attack, int defense)
        {
            _summoned = true;
            this.attack = attack;
            this.defense = defense;
        }
        
        public Pair<MonsterCrests, Sprite> GetRandomFace()
        {
            return Faces[Random.Range(0, Faces.Count)];
        }
    }
}
