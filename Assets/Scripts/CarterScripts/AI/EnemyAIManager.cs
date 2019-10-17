using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static AIBehaviours;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] List<EnemyAI> agents;

    IBehaviour chaserBehaviour;

    List<IBehaviour> PopulateBranch(params IBehaviour[] children)
    {
        List<IBehaviour> behaviours = children.ToList();
        return behaviours;
    }

    IBehaviour PopulateBehaviours(EnemyAI.Type agentType)
    {
        var root = new SelectorNode();

        switch (agentType)
        {
            case EnemyAI.Type.Chaser:
                //root
                var ramPath = new SequenceNode();
                var chasePath = new SequenceNode();
                root.childBehaviors = PopulateBranch(ramPath, chasePath);

                //ramPath
                IBehaviour[] ramPathChildren = { new AgentShouldRam(), new AgentRamTarget() };
                ramPath.childBehaviors = PopulateBranch(ramPathChildren[0], ramPathChildren[1]);

                //chasePath
                IBehaviour[] chasePathChildren = { new AgentShouldChase(), new AgentChaseTarget() };
                chasePath.childBehaviors = PopulateBranch(chasePathChildren[0], chasePathChildren[1]);

                break;
        }
        return root;
    }

    private void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            agents.Add(g.GetComponent<EnemyAI>());
        }

        chaserBehaviour = PopulateBehaviours(EnemyAI.Type.Chaser);
    }


    void Update()
    {
        foreach(EnemyAI agent in agents)
        {
            switch (agent.type)
            {
                case EnemyAI.Type.Chaser:
                    chaserBehaviour.DoBehaviour(agent);
                    break;
            }
        }
    }
}
