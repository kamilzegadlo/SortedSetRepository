using System;

namespace Interview
{
    public interface IStoreable : ICloneable
    {
        IComparable Id { get; }

        void Update(IStoreable newItem);
    }
    
}