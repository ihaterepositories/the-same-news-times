using System;
using UnityEngine;

namespace Interfaces
{
    public interface IPoolAble
    {
        GameObject GameObject { get; }
        event Action<IPoolAble> OnDestroyed;
        void Reset();
    }
}
