using System;
using UnityEngine;

namespace Pooling.Interfaces
{
    public interface IPoolAble
    {
        GameObject GameObject { get; }
        event Action<IPoolAble> OnDestroyed;
        void Reset();
    }
}
