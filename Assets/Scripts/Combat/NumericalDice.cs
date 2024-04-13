using System;
using System.Collections.Generic;
using DataStructures;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat
{
    [Serializable]
    public class NumericalDice
    {
        [HideInInspector] public string diceName;
        
        private readonly List<Pair<int, Sprite>> _faces;

        public NumericalDice(string diceName, List<Pair<int, Sprite>> faces)
        {
            this.diceName = diceName;
            _faces = faces;
        }
        
        public Pair<int, Sprite> GetRandomFace()
        {
            return _faces[Random.Range(0, _faces.Count)];
        }
    }
}