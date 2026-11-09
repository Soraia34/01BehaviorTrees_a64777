using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject diamond;
    public GameObject van;
    NavMeshAgent agent;

    public enum ActionSate {IDLE, WORKING};
    ActionSate state = ActionSate.IDLE;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Node steal = new Node("Steal Something");
        Leaf goToDiamond = new Leaf("Go to Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go to Van", GoToVan);

        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        tree.AddChild(steal);

        /*Node eat = new Node("Eat Something");
        Node pizza = new Node("Go To Pizza Shop");
        Node buy = new Node("Buy Pizza");

        eat.AddChild(pizza);
        eat.AddChild(buy);
        tree.AddChild(eat);*/

        tree.PrintTree();
        tree.Process();
    }

    public Node.Status GoToDiamond()
    {
        return GoToLocation(diamond.transform.position);
    }

    public Node.Status GoToVan()
    {
        return GoToLocation(van.transform.position);
    }

    Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        if (state == ActionSate.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionSate.WORKING;
        }
        else if (Vector3.Distance (agent.pathEndPosition, destination) >= 2)
        {
            state = ActionSate.IDLE;
            return Node.Status.FAILURE;
        }
        else if (distanceToTarget < 2)
        {
            state = ActionSate.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
