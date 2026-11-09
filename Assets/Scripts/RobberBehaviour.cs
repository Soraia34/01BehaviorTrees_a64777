using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tree = new BehaviourTree();
        Node steal = new Node("Steal Something");
        Node goToDiamond = new Node("Go to Diamond");
        Node goToVan = new Node("Go to Van");

        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        tree.AddChild(steal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
