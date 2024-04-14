using System.Collections.Generic;
using Dice;
using Extensions;
using PlayerState;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class MonsterDiceHolder : MonoBehaviour
    {
        public MonsterDiceSO dice;

        public List<Image> faces;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;

        public Button buyButton;

        private bool _initiated;
        
        private void Start()
        {
            buyButton.onClick.AddListener(BuyItem);
        }

        public void InitiateUI()
        {
            nameText.text = dice.diceName;
            priceText.text = dice.price.ToString();

            for (int i = 0; i < faces.Count; i++)
            {
                faces[i].sprite = dice.faces[i].secondMember;
            }

            _initiated = true;
        }

        private void Update()
        {
            if (_initiated) buyButton.interactable = PlayerInventory.Instance.currentGold >= dice.price;
        }

        private void BuyItem()
        {
            if (!_initiated) return;
            PlayerInventory.Instance.currentGold -= dice.price;
            PlayerInventory.Instance.AddMonster(dice);
            _initiated = false;
            List<MonsterDiceSO> monsterDicePool = PlayerInventory.Instance.fullMonsterDicePool;
            dice = monsterDicePool[Random.Range(0, monsterDicePool.Count)].Clone();
            InitiateUI();
        }
    }
}