using System.Collections.Generic;
using System.Linq;
using Dice;
using UnityEngine;

namespace PlayerState
{
    public class PlayerInventory : MonoBehaviour
    {
        #region SingleTon
        
        public static PlayerInventory Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        public List<MonsterDiceSO> fullMonsterDicePool;
        public List<NumericalDiceSO> fullNumericalDicePool;
        
        public List<MonsterDiceSO> monsterBag;
        public List<NumericalDiceSO> numericalBag;
        public int currentGold;
        public int minNumMonsterDice = 3;
        public int minNumNumericalDice = 2;
        
        private void Start()
        {
            monsterBag = new List<MonsterDiceSO>();
            numericalBag = new List<NumericalDiceSO>();
        }

        public void AddMonster(MonsterDiceSO monster)
        {
            monsterBag.Add(monster);
            monsterBag = monsterBag.OrderBy(m => m.diceName).ToList();

        }

        public void RemoveMonster(MonsterDiceSO monsterDiceSO)
        {
            if (monsterBag.Count <= minNumMonsterDice) return;
            monsterBag.Remove(monsterDiceSO);
        }

        public void AddNumerical(NumericalDiceSO numericalDiceSO)
        {
            numericalBag.Add(numericalDiceSO);
            numericalBag = numericalBag.OrderBy(n => n.diceName).ToList();
        }

        public void RemoveNumerical(NumericalDiceSO numericalDiceSO)
        {
            if (numericalBag.Count <= minNumNumericalDice) return;
            numericalBag.Remove(numericalDiceSO);
        }
    }
}