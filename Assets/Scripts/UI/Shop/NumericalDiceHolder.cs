using System.Collections.Generic;
using Dice;
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
        }

        private void Update()
        {
            buyButton.interactable = PlayerInventory.Instance.currentGold >= dice.price;
        }

        private void BuyItem()
        {
            PlayerInventory.Instance.currentGold -= dice.price;
            PlayerInventory.Instance.AddNumerical(dice);
            Destroy(gameObject);
        }
    }
}