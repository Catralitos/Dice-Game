using System.Collections.Generic;
using DataStructures;
using UnityEngine;

namespace Dice
{
    [CreateAssetMenu(menuName = "Dice/Numerical Dice")]
    public class NumericalDiceSO : ScriptableObject
    {
        public string diceName;

        public int price;
        
        public List<Pair<int, Sprite>> faces;
    }
}