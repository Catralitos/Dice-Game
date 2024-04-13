using System.Collections.Generic;
using Dice;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private List<MonsterDiceSO> monsterDicePool;
        [SerializeField] private List<NumericalDiceSO> numericalDicePool;

        public List<MonsterDiceHolder> monsterDiceHolders;
        public List<NumericalDiceHolder> numericalDiceHolders;

        public Button nextRoundButton;
        
        private void Start()
        {
            foreach (MonsterDiceHolder holder in monsterDiceHolders)
            {
                holder.dice = monsterDicePool[Random.Range(0, monsterDicePool.Count)];
                holder.InitiateUI();
            }
            
            foreach (NumericalDiceHolder holder in numericalDiceHolders)
            {
                holder.dice = numericalDicePool[Random.Range(0, numericalDicePool.Count)];
                holder.InitiateUI();
            }
            
            nextRoundButton.onClick.AddListener(NextRound);
        }

        private void NextRound()
        {
            Debug.Log("Loading next round;");
        }
    }
}