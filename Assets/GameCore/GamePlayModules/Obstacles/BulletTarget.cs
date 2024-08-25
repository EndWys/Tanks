using System;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    public event Action OnBulletHit = () => { };
    public void TakeHit()
    {
        OnBulletHit.Invoke();
    }
}
