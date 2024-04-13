using System;

namespace DataStructures
{
    [Serializable]
    public class Pair<T,TU>
    {
        public T firstMember;
        public TU secondMember;

        public Pair(T firstMember, TU secondMember)
        {
            this.firstMember = firstMember; 
            this.secondMember = secondMember;
        }
    }
}