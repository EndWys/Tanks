using Assets.CodeUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.Other.PoolingSystem
{
    public class Pooling<T> : List<T> where T : CachedMonoBehaviour, IPooling
    {
        private Transform _parent;
        private Vector3 _startPos;
        private GameObject _referenceObject;

        public bool CreateMoreIfNeeded = true;
        public delegate void ObjectCreationCallback(T obj);
        public event ObjectCreationCallback OnObjectCreationCallBack;

        public Pooling<T> Initialize(GameObject refObject, Transform parent)
        {
            return Initialize(0, refObject, parent);
        }

        public Pooling<T> Initialize(int amount, GameObject refObject, Transform parent, bool startState = false)
        {
            return Initialize(amount, refObject, parent, Vector3.zero, startState);
        }

        public Pooling<T> Initialize(int amount, GameObject refObject, Transform parent, Vector3 worldPos, bool startState = false)
        {
            _parent = parent;
            _startPos = worldPos;
            _referenceObject = refObject;

            Clear();

            for (var i = 0; i < amount; i++)
            {
                var obj = CreateObject();

                if (startState) obj.OnCollect();
                else obj.OnRelease();

                Add(obj);
            }

            return this;
        }

        public T Collect(Transform parent = null, Vector3? position = null, bool localPosition = true, Quaternion? rotation = null, bool localRotation = true)
        {
            var obj = Find(x => x.IsUsing == false);
            if (obj == null && CreateMoreIfNeeded)
            {
                obj = CreateObject(parent, position);
                Add(obj);
            }

            if (obj == null) return obj;

            obj.CachedTransform.SetParent(parent ?? _parent);

            if (localPosition)
                obj.CachedTransform.localPosition = position ?? _startPos;
            else
                obj.CachedTransform.position = position ?? _startPos;

            if (localRotation)
                obj.CachedTransform.localRotation = rotation ?? Quaternion.identity;
            else
                obj.CachedTransform.rotation = rotation ?? Quaternion.identity;

            obj.OnCollect();

            return obj;
        }

        public void Release(T obj)
        {
            if (obj != null)
                obj.OnRelease();
        }

        public void ReleaseAll()
        {
            ForEach(Release);
        }

        public List<T> GetAllWithState(bool active)
        {
            return FindAll(x => x.IsUsing == active);
        }

        private T CreateObject(Transform parent = null, Vector3? position = null)
        {
            var obj = Object.Instantiate(_referenceObject, position ?? _startPos, Quaternion.identity, parent ?? _parent).GetComponent<T>();
            obj.CachedTransform.localPosition = position ?? _startPos;
            obj.name = obj.ObjectName + Count;

            if (OnObjectCreationCallBack != null)
                OnObjectCreationCallBack.Invoke(obj);

            return obj;
        }
    }
}