using System.Collections.Generic;
using DataStructures;
using UnityEngine;

namespace Dice
{
    public enum MonsterCrests
    {
       Special,
       Attack,
       Heal
    }
    
    [CreateAssetMenu(menuName = "Dice/Monster Dice")]
    public class MonsterDice : ScriptableObject
    {
        public string diceName;

        public MonsterStats monsterStats;
    
        public List<Pair<MonsterCrests, Sprite>> faces;

        public Pair<MonsterCrests, Sprite> GetRandomFace()
        {
            return faces[Random.Range(0, faces.Count)];
        }
    }
}