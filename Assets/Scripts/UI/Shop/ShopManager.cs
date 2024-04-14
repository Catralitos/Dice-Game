using System.Collections.Generic;
using Dice;
using Extensions;
using PlayerState;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Dice Holders")]
        public List<MonsterDiceHolder> monsterDiceHolders;
        public List<NumericalDiceHolder> numericalDiceHolders;

        [Header("Dice Holder Prefabs")] 
        public GameObject monsterDiceDestroyHolderPrefab;
        public GameObject numericalDiceDestroyHolderPrefab;

        [Header("Buttons")]
        public Button nextRoundButton;
        public Button destroyMonsterButton;
        public Button destroyNumericalButton;
        public Button backOutOfMonsterDestroyButton;
        public Button backOutOfNumericalDestroyButton;
        
        [Header("Screens")]
        public GameObject shopScreen;
        public GameObject destroyMonsterScreen;
        public GameObject destroyNumericalScreen;

        [Header("Content Viewports")] 
        public GameObject destroyMonsterContentViewport;
        public GameObject destroyNumericalContentViewport;
        
        [Header("Gold Texts")] 
        public TextMeshProUGUI currentGoldText;
        
        private void Start()
        {
            foreach (MonsterDiceHolder holder in monsterDiceHolders)
            {
                List<MonsterDiceSO> monsterDicePool = PlayerInventory.Instance.fullMonsterDicePool;
                holder.dice = monsterDicePool[Random.Range(0, monsterDicePool.Count)].Clone();
                holder.InitiateUI();
            }
            
            foreach (NumericalDiceHolder holder in numericalDiceHolders)
            {
                List<NumericalDiceSO> numericalDicePool = PlayerInventory.Instance.fullNumericalDicePool;
                holder.dice = numericalDicePool[Random.Range(0, numericalDicePool.Count)].Clone();
                holder.InitiateUI();
            }
            
            nextRoundButton.onClick.AddListener(NextRound);
            destroyMonsterButton.onClick.AddListener(OpenDestroyMonsterScreen);
            destroyNumericalButton.onClick.AddListener(OpenDestroyNumericalScreen);
            backOutOfMonsterDestroyButton.onClick.AddListener(OpenShopScreen);
            backOutOfNumericalDestroyButton.onClick.AddListener(OpenShopScreen);
        }

        private void Update()
        {
            currentGoldText.text = PlayerInventory.Instance.currentGold.ToString();
            destroyMonsterButton.interactable = PlayerInventory.Instance.monsterBag.Count >= 4;
            destroyNumericalButton.interactable = PlayerInventory.Instance.numericalBag.Count >= 3;
            if (destroyMonsterScreen.activeSelf && PlayerInventory.Instance.monsterBag.Count <= PlayerInventory.Instance.minNumMonsterDice) OpenShopScreen();
            if (destroyNumericalScreen.activeSelf && PlayerInventory.Instance.numericalBag.Count <= PlayerInventory.Instance.minNumNumericalDice) OpenShopScreen();
        }

        private static void NextRound()
        {
            Debug.Log("Loading next round;");
        }

        private void OpenDestroyMonsterScreen()
        {
            shopScreen.gameObject.SetActive(false);
            destroyNumericalScreen.SetActive(false);
            
            foreach (MonsterDiceSO monsterDiceSO in PlayerInventory.Instance.monsterBag)
            {
                GameObject spawnedMonsterDiceDestroyHolder = Instantiate(monsterDiceDestroyHolderPrefab,
                    destroyMonsterContentViewport.transform);
                MonsterDiceDestroyHolder monDiceDestroyHolder =
                    spawnedMonsterDiceDestroyHolder.GetComponent<MonsterDiceDestroyHolder>();
                monDiceDestroyHolder.dice = monsterDiceSO;
                monDiceDestroyHolder.InitiateUI();
            }
            
            destroyMonsterScreen.SetActive(true);
        }
        
        private void OpenDestroyNumericalScreen()
        {
            shopScreen.gameObject.SetActive(false);
            destroyMonsterScreen.SetActive(false);

            foreach (NumericalDiceSO numericalDiceSO in PlayerInventory.Instance.numericalBag)
            {
                GameObject spawnedNumericalDiceDestroyHolder = Instantiate(numericalDiceDestroyHolderPrefab,
                    destroyNumericalContentViewport.transform);
                NumericalDiceDestroyHolder numDiceDestroyHolder =
                    spawnedNumericalDiceDestroyHolder.GetComponent<NumericalDiceDestroyHolder>();
                numDiceDestroyHolder.dice = numericalDiceSO;
                numDiceDestroyHolder.InitiateUI();
            }
            
            destroyNumericalScreen.SetActive(true);
        }

        private void OpenShopScreen()
        {
            destroyMonsterScreen.SetActive(false);
            destroyNumericalScreen.SetActive(false);
            foreach(Transform child in destroyMonsterContentViewport.transform)
            {
                Destroy(child.gameObject);
            }
            foreach(Transform child in destroyNumericalContentViewport.transform)
            {
                Destroy(child.gameObject);
            }
            shopScreen.gameObject.SetActive(true);
        }
    }
}