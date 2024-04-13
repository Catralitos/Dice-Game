using System.Collections.Generic;
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
        
        public List<MonsterDiceSO> monsterBag;
        public List<NumericalDiceSO> numericalBag;
        public int currentGold;
        
        private void Start()
        {
            monsterBag = new List<MonsterDiceSO>();
            numericalBag = new List<NumericalDiceSO>();
        }

        public void AddMonster(MonsterDiceSO monster)
        {
            monsterBag.Add(monster);
        }

        public void RemoveMonster(MonsterDiceSO monsterDiceSO)
        {
            monsterBag.Remove(monsterDiceSO);
        }

        public void AddNumerical(NumericalDiceSO numericalDiceSO)
        {
            numericalBag.Add(numericalDiceSO);
        }

        public void RemoveNumerical(NumericalDiceSO numericalDiceSO)
        {
            numericalBag.Remove(numericalDiceSO);
        }
    }
}