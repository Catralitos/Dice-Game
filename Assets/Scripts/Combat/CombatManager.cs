using System;
using System.Collections.Generic;
using Combat;
using DataStructures;
using Dice;
using PlayerState;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CombatManager : MonoBehaviour
    {

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
        
        [HideInInspector] public bool isAI;
        private bool _pickingDice;

        public List<MonsterDiceUI> MonsterDiceUis;
        public List<NumericalDiceUI> NumericalDiceUis;
        
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
        _monsterDeck = PlayerInventory.Instance.fullMonsterDicePool;
        _numericalDeck = PlayerInventory.Instance.fullNumericalDicePool;
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
                        _monstersInPlay.Add(new MonsterDice(toDraw.diceName, toDraw.faces));
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
                        _monstersInPlay.Add(new MonsterDice(toDraw.diceName, toDraw.faces));
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
                        _monstersInPlayEnemy.Add(new MonsterDice(toDraw.diceName, toDraw.faces));
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
                        _monstersInPlayEnemy.Add(new MonsterDice(toDraw.diceName, toDraw.faces));
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
            if (!_pickingDice) return;
        }

        private void PickNumericalDice()
        {
            if (!isAI)
            {
                _pickingDice = true;
            }
            else
            {
                ResolveEffects();
            }
        }

        private void ResolveEffects()
        {
            foreach (MonsterCrests crest in _rolledCrests)
            {
                switch (_rolledCrests)
                {
                    case MonsterCrests.Attack:
                    
                    break;
                    case MonsterCrests.Defense:
                    
                    break;
                    case MonsterCrests.Heal:
                    
                    break;
                    
                    default:
                    
                    break;
                }
            }
        }
    }
}