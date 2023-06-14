namespace TetrisRemake.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Patterns.ObjectPool.Interfaces;

    public class SpawnManager : PoolsManager
    {
        public GameObject GetRandomPiece()
        {
            Pool pool = GetRandomPool();
            GameObject go = pool.Get();

            if(go.TryGetComponent(out IPooledObject po))
            {
                po.AssociatePool(pool);
            }

            return go;
        }

        public Pool GetRandomPool()
        {
            if (Pools.Dict.Count == 0){
                return null;
            }

            List<string> keyList = Pools.Dict.Keys.ToList();
            int randomIndex = new System.Random().Next(0, keyList.Count);
            string randomKey = keyList[randomIndex];

            return Pools[randomKey];
        }
    }
}