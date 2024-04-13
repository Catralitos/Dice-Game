using System.Collections.Generic;
using DataStructures;
using UnityEngine;

namespace Dice
{
    [CreateAssetMenu(menuName = "Dice/Numerical Dice")]
    public class NumericalDice : ScriptableObject
    {
        public string diceName;

        public List<Pair<int, Sprite>> faces;

        public Pair<int, Sprite> GetRandomFace()
        {
            return faces[Random.Range(0, faces.Count)];
        }
        
    }
}