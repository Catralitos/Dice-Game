using System.Collections.Generic;
using Dice;
using Extensions;
using PlayerState;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class NumericalDiceHolder : MonoBehaviour
    {
        public NumericalDiceSO dice;

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
            PlayerInventory.Instance.AddNumerical(dice);
            _initiated = false;
            List<NumericalDiceSO> numericalDicePool = PlayerInventory.Instance.fullNumericalDicePool;
            dice = numericalDicePool[Random.Range(0, numericalDicePool.Count)].Clone();
            InitiateUI();
        }
    }
}