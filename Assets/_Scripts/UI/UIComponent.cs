using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIComponent : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    protected abstract void Setup();
    protected abstract void Configure();

    private void Init()
    {
        Setup();
        Configure();
    }

    private void OnValidate()
    {
        Init();
    }
}
