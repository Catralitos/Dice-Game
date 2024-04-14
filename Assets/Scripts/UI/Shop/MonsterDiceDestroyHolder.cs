using System.Collections.Generic;
using Dice;
using PlayerState;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class MonsterDiceDestroyHolder : MonoBehaviour
    {
        public MonsterDiceSO dice;

        public List<Image> faces;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI sellPriceText;

        public Button sellButton;

        private bool _initiated;
        
        private void Start()
        {
            sellButton.onClick.AddListener(SellItem);
        }

        public void InitiateUI()
        {
            nameText.text = dice.diceName;
            sellPriceText.text = (dice.price / 2).ToString();

            for (int i = 0; i < faces.Count; i++)
            {
                faces[i].sprite = dice.faces[i].secondMember;
            }

            _initiated = true;
        }
        
        private void SellItem()
        {
            if (!_initiated) return;
            PlayerInventory.Instance.currentGold += dice.price / 2;
            PlayerInventory.Instance.RemoveMonster(dice);
            Destroy(gameObject);
        }
    }
}