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
    public class MonsterDiceSO : ScriptableObject
    {
        public string diceName;

        public int price;
        
        public MonsterStats monsterStats;
    
        public List<Pair<MonsterCrests, Sprite>> faces;

        public Sprite monsterSprite;

    }
}