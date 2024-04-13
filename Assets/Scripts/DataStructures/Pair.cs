using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DataStructures
{
    [Serializable]
    public class Pair<T,U>
    {
        public T firstMember;
        public U secondMember;

        public Pair(T firstMember, U secondMember)
        {
            this.firstMember = firstMember; 
            this.secondMember = secondMember;
        }
    }
}