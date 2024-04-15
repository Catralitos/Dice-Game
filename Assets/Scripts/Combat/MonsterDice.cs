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

        public Sprite monsterSprite;
        
        public bool summoned;
        
        public MonsterDice(string monsterName, List<Pair<MonsterCrests, Sprite>> faces, Sprite monsterSprite)
        {
            this.monsterName = monsterName;
            this.Faces = faces;
            this.monsterSprite = monsterSprite;
        }

        public void Summon(int attack, int defense)
        {
            summoned = true;
            this.attack = attack;
            this.defense = defense;
        }
        
        public Pair<MonsterCrests, Sprite> GetRandomFace()
        {
            return Faces[Random.Range(0, Faces.Count)];
        }
    }
}
