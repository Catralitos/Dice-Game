using System;
using System.Collections.Generic;
using DataStructures;
using Dice;
using PlayerState;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Combat
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance { get; private set; }
        
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
        }
        
        private List<MonsterDiceSO> _monsterDeck;
        private List<NumericalDiceSO> _numericalDeck;
        private List<MonsterDiceSO> _monsterDeckEnemy;
        private List<NumericalDiceSO> _numericalDeckEnemy;
        
        private List<MonsterDice> _monstersInPlay;
        private List<NumericalDice> _numericalInPlay;
        private List<MonsterDice> _monstersInPlayEnemy;
        private List<NumericalDice> _numericalInPlayEnemy;
        
        private List<MonsterDiceSO> _monstersInPlaySO;
        private List<NumericalDiceSO> _numericalInPlaySO;
        private List<MonsterDiceSO> _monstersInPlayEnemySO;
        private List<NumericalDiceSO> _numericalInPlayEnemySO;
        
        private List<MonsterDiceSO> _monsterGraveyard;
        private List<NumericalDiceSO> _numericalGraveyard;
        private List<MonsterDiceSO> _monsterGraveyardEnemy;
        private List<NumericalDiceSO> _numericalGraveyardEnemy;

        private List<MonsterCrests> _rolledCrests;
        private List<int> _rolledNumbers;

        [HideInInspector] public bool isAI;
        [HideInInspector] public bool pickingDice;
        [HideInInspector] public int assignedDice;
        
        public List<MonsterDiceUI> MonsterDiceUis;
        public List<NumericalDiceUI> NumericalDiceUis;
        
        //TODO talvez nao seja image, por causa de anims e tal
        public List<Image> playerMonsters;
        public List<Image> enemyMonsters;

        public GameObject playerMonsterGraveyard;
        public GameObject playerNumericalGraveyard;
        public GameObject enemyMonsterGraveyard;
        public GameObject enemyNumericalGraveyard;
        private void Start()
        {

            //gerar um deck inimigo a partir da pool de dados no playerinventory
            //gerar conforme dificuldade
            //PlayerInventory.Instance

            // Inicializar listas
            _monstersInPlaySO = new List<MonsterDiceSO>();
            _numericalInPlaySO = new List<NumericalDiceSO>();
            _monstersInPlayEnemySO = new List<MonsterDiceSO>();
            _numericalInPlayEnemySO = new List<NumericalDiceSO>();
            _monsterGraveyard = new List<MonsterDiceSO>();
            _numericalGraveyard = new List<NumericalDiceSO>();
            _monsterGraveyardEnemy = new List<MonsterDiceSO>();
            _numericalGraveyardEnemy = new List<NumericalDiceSO>();
            _rolledCrests = new List<MonsterCrests>();

            // obter decks do PlayerInventory
            _monsterDeck = PlayerInventory.Instance.monsterBag;
            _numericalDeck = PlayerInventory.Instance.numericalBag;
            
            //GERAR DECKS A PARTIR DO INVENTÁRIO. E PARA OS INIMIGOS
            _monsterDeckEnemy = new List<MonsterDiceSO>();
            _numericalDeckEnemy = new List<NumericalDiceSO>();

            StartTurn();
        }

        private void StartTurn()
        {
            if (!isAI)
            {
                int numMonToDraw = 3 - _monstersInPlay.Count;
                if (numMonToDraw > 0)
                {
                    int drawnMon = 0;
                    for (int i = 0; i < numMonToDraw || i < _monsterDeck.Count; i++)
                    {
                        MonsterDiceSO toDraw = _monsterDeck[Random.Range(0, _monsterDeck.Count)];
                        _monsterDeck.Remove(toDraw);
                        _monstersInPlaySO.Add(toDraw);
                        _monstersInPlay.Add(new MonsterDice(toDraw.diceName, toDraw.faces, toDraw.monsterSprite));
                        drawnMon++;
                    }

                    if (drawnMon < numMonToDraw)
                    {
                        foreach (MonsterDiceSO monster in _monsterGraveyard)
                        {
                            _monsterDeck.Add(monster);
                        }

                        _monsterGraveyard.Clear();
                    }

                    numMonToDraw -= drawnMon;

                    for (int i = 0; i < numMonToDraw || i < _monsterDeck.Count; i++)
                    {
                        MonsterDiceSO toDraw = _monsterDeck[Random.Range(0, _monsterDeck.Count)];
                        _monsterDeck.Remove(toDraw);
                        _monstersInPlaySO.Add(toDraw);
                        _monstersInPlay.Add(new MonsterDice(toDraw.diceName, toDraw.faces, toDraw.monsterSprite));
                    }
                }

                int numNumToDraw = _monstersInPlay.Count;
                if (numNumToDraw > 0)
                {
                    int drawnNum = 0;
                    for (int i = 0; i < numNumToDraw || i < _numericalDeck.Count; i++)
                    {
                        NumericalDiceSO toDraw = _numericalDeck[Random.Range(0, _numericalDeck.Count)];
                        _numericalDeck.Remove(toDraw);
                        _numericalInPlaySO.Add(toDraw);
                        _numericalInPlay.Add(new NumericalDice(toDraw.diceName, toDraw.faces));
                        drawnNum++;
                    }

                    if (drawnNum < numNumToDraw)
                    {
                        foreach (NumericalDiceSO numerical in _numericalGraveyard)
                        {
                            _numericalDeck.Add(numerical);
                        }

                        _numericalDeck.Clear();
                    }

                    numNumToDraw -= drawnNum;

                    for (int i = 0; i < numNumToDraw || i < _numericalDeck.Count; i++)
                    {
                        NumericalDiceSO toDraw = _numericalDeck[Random.Range(0, _numericalDeck.Count)];
                        _numericalDeck.Remove(toDraw);
                        _numericalInPlaySO.Add(toDraw);
                        _numericalInPlay.Add(new NumericalDice(toDraw.diceName, toDraw.faces));
                    }
                }

            }
            else
            {
                int numMonToDraw = 3 - _monstersInPlayEnemy.Count;
                if (numMonToDraw > 0)
                {
                    int drawnMon = 0;
                    for (int i = 0; i < numMonToDraw || i < _monsterDeckEnemy.Count; i++)
                    {
                        MonsterDiceSO toDraw = _monsterDeck[Random.Range(0, _monsterDeckEnemy.Count)];
                        _monsterDeckEnemy.Remove(toDraw);
                        _monstersInPlayEnemySO.Add(toDraw);
                        _monstersInPlayEnemy.Add(new MonsterDice(toDraw.diceName, toDraw.faces, toDraw.monsterSprite));
                        drawnMon++;
                    }

                    if (drawnMon < numMonToDraw)
                    {
                        foreach (MonsterDiceSO monster in _monsterGraveyardEnemy)
                        {
                            _monsterDeckEnemy.Add(monster);
                        }

                        _monsterGraveyardEnemy.Clear();
                    }

                    numMonToDraw -= drawnMon;

                    for (int i = 0; i < numMonToDraw || i < _monsterDeckEnemy.Count; i++)
                    {
                        MonsterDiceSO toDraw = _monsterDeck[Random.Range(0, _monsterDeckEnemy.Count)];
                        _monsterDeckEnemy.Remove(toDraw);
                        _monstersInPlayEnemySO.Add(toDraw);
                        _monstersInPlayEnemy.Add(new MonsterDice(toDraw.diceName, toDraw.faces, toDraw.monsterSprite));
                    }
                }

                int numNumToDraw = _monstersInPlayEnemy.Count;
                if (numNumToDraw > 0)
                {
                    int drawnNum = 0;
                    for (int i = 0; i < numNumToDraw || i < _numericalDeckEnemy.Count; i++)
                    {
                        NumericalDiceSO toDraw = _numericalDeckEnemy[Random.Range(0, _numericalDeckEnemy.Count)];
                        _numericalDeckEnemy.Remove(toDraw);
                        _numericalInPlayEnemySO.Add(toDraw);
                        _numericalInPlayEnemy.Add(new NumericalDice(toDraw.diceName, toDraw.faces));
                        drawnNum++;
                    }

                    if (drawnNum < numNumToDraw)
                    {
                        foreach (NumericalDiceSO numerical in _numericalGraveyardEnemy)
                        {
                            _numericalDeckEnemy.Add(numerical);
                        }

                        _numericalDeckEnemy.Clear();
                    }

                    numNumToDraw -= drawnNum;

                    for (int i = 0; i < numNumToDraw || i < _numericalDeckEnemy.Count; i++)
                    {
                        NumericalDiceSO toDraw = _numericalDeckEnemy[Random.Range(0, _numericalDeckEnemy.Count)];
                        _numericalDeckEnemy.Remove(toDraw);
                        _numericalInPlayEnemySO.Add(toDraw);
                        _numericalInPlayEnemy.Add(new NumericalDice(toDraw.diceName, toDraw.faces));
                    }
                }
            }
            RollMonsterDice();
        }

        private void RollMonsterDice()
        {
            if (!isAI)
            {
                for (int i = 0; i < _monstersInPlay.Count; i++)
                {
                    Pair<MonsterCrests, Sprite> rolledFace = _monstersInPlay[i].GetRandomFace();
                    MonsterDiceUis[i].SetFace(rolledFace.secondMember);
                    _rolledCrests.Add(rolledFace.firstMember);
                }
                
                for (int i = 0; i < _numericalInPlay.Count; i++)
                {
                    NumericalDiceUis[i].SetDice(_numericalInPlay[i]);
                }
            }
            else
            {
                for (int i = 0; i < _monstersInPlayEnemy.Count; i++)
                {
                    Pair<MonsterCrests, Sprite> rolledFace = _monstersInPlayEnemy[i].GetRandomFace();
                    MonsterDiceUis[i].SetFace(rolledFace.secondMember);
                    _rolledCrests.Add(rolledFace.firstMember);                }
                
                for (int i = 0; i < _numericalInPlayEnemy.Count; i++)
                {
                    NumericalDiceUis[i].SetDice(_numericalInPlayEnemy[i]);
                }
            }
            PickNumericalDice();
        }

        private void Update()
        {
            if (isAI)
            {
                playerMonsterGraveyard.SetActive(false);
                playerNumericalGraveyard.SetActive(false);
                enemyMonsterGraveyard.SetActive(true);
                enemyNumericalGraveyard.SetActive(true);
            }
            else
            {
                enemyMonsterGraveyard.SetActive(false);
                enemyNumericalGraveyard.SetActive(false);
                playerMonsterGraveyard.SetActive(true);
                playerNumericalGraveyard.SetActive(true);
            }
            
            for (int i = 0; i < _monstersInPlay.Count; i++)
            {
                if (_monstersInPlay[i].summoned)
                {
                    playerMonsters[i].sprite = _monstersInPlay[i].monsterSprite;
                    playerMonsters[i].enabled = true;
                }
                else
                {
                    playerMonsters[i].enabled=false;
                }
            }
            
            for (int i = 0; i < _monstersInPlayEnemy.Count; i++)
            {
                if (_monstersInPlayEnemy[i].summoned)
                {
                    enemyMonsters[i].sprite = _monstersInPlayEnemy[i].monsterSprite;
                    enemyMonsters[i].enabled = true;
                }
                else
                {
                    enemyMonsters[i].enabled = false;
                }
            }
            
            if (!pickingDice) return;
            if (assignedDice != _monstersInPlay.Count) return;
            pickingDice = false; 
            RollNumerical();
        }

        private void PickNumericalDice()
        {
            if (!isAI)
            {
                pickingDice = true;
            }
            else
            {
                List<NumericalDice> pickedDice = new List<NumericalDice>();
                foreach (MonsterDiceUI monster in MonsterDiceUis)
                {
                    int c = 0;
                    while (c <= 1000)
                    {
                        monster.assignedDice = _numericalInPlayEnemy[Random.Range(0, _numericalInPlayEnemy.Count)];
                        if (!pickedDice.Contains(monster.assignedDice))
                        {
                            pickedDice.Add(monster.assignedDice);
                            break;
                        }
                        c++;
                    }
                }
                RollNumerical();
            }
        }

        private void RollNumerical()
        {
            foreach (MonsterDiceUI diceUI in MonsterDiceUis)
            {
                Pair<int, Sprite> rolledFace = diceUI.assignedDice.GetRandomFace();
                diceUI.numericalDiceFace.sprite = rolledFace.secondMember;
                _rolledNumbers.Add(rolledFace.firstMember);
            }
            ResolveEffects();
        }

        private void ResolveEffects()
        {
            for (int i = 0 ; i < _rolledCrests.Count; i++)
            {
                //com o indice correspondente no _rolledInts fazer os efeits
                //o special pode dar summon, como pode dar efeito conforme o nome do dado, tem que se comparar
                switch (_rolledCrests[i])
                {
                    case MonsterCrests.Attack:
                    
                        break;
                    case MonsterCrests.Special:
                    
                        break;
                    case MonsterCrests.Heal:
                    
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            EndTurn();
        }

        private void EndTurn()
        {
            _rolledCrests.Clear();
            _rolledNumbers.Clear();
            foreach (MonsterDiceUI monster in MonsterDiceUis)
            {
                monster.assignedDice = null;
                monster.numericalDiceFace.enabled = false;
            }

            foreach (MonsterDice t in _monstersInPlay)
            {
                if (t.defense <= 0)
                {
                    //TODO send to graveyard
                    _monstersInPlay.Remove(t);
                }
            }
            
            foreach (MonsterDice t in _monstersInPlayEnemy)
            {
                if (t.defense <= 0)
                {
                    //TODO send to graveyard
                    _monstersInPlayEnemy.Remove(t);
                }
            }
            
            foreach (NumericalDice t in _numericalInPlay)
            {
                //TODO send to graveyard
                _numericalInPlay.Remove(t);
            }
        }
    }
}