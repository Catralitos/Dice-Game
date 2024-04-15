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
        
        public readonly List<Pair<int, Sprite>> Faces;

        public NumericalDice(string diceName, List<Pair<int, Sprite>> faces)
        {
            this.diceName = diceName;
            Faces = faces;
        }
        
        public Pair<int, Sprite> GetRandomFace()
        {
            return Faces[Random.Range(0, Faces.Count)];
        }
    }
}