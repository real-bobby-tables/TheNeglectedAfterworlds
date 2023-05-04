using System;
using System.Collections.Generic;
using UnityEngine;

/* https://www.toptal.com/unity-unity3d/unity-ai-development-finite-state-machine-tutorial */


public class BaseStateMachine : MonoBehaviour
{

    [SerializeField] private BaseState _initialState;
    private Dictionary<Type, Component> _cachedComponents;

    private GameObject Player;

    private void Awake()
    {
        CurrentState = _initialState;
        _cachedComponents = new Dictionary<Type, Component>();
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public BaseState CurrentState { get; set; }

    private void Update()
    {
        CurrentState.Execute(this);
    }

    private void FixedUpdate()
    {
        CurrentState.FixedExecute(this);
    }

    public new T GetComponent<T>() where T : Component 
    {
        if(_cachedComponents.ContainsKey(typeof(T)))
        {
            return _cachedComponents[typeof(T)] as T;
        }


        var component = base.GetComponent<T>();
        if(component != null)
        {
            _cachedComponents.Add(typeof(T), component);
        }
        return component;
    }

    public GameObject GetPlayer()
    {
        return Player;
    }
}
