using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainGameUI : MonoBehaviour
{
    private VisualElement root;
    private void Awake()
    {
        
        root = GetComponent<UIDocument>().rootVisualElement;
        
    }

    public void UpdateSoulCount(int count)
    {
        root.Q<Label>("NumSouls").text = "" + count;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
