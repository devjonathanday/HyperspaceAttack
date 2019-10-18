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
                var blockedPath = new SequenceNode();
                var ramPath = new SequenceNode();
                var chasePath = new SequenceNode();
                var brakePath = new SequenceNode();
                var seekPath = new SequenceNode();
                var reallignPath = new SequenceNode();
                root.childBehaviors = PopulateBranch(blockedPath, ramPath, chasePath, brakePath, seekPath, reallignPath);

                //blockedPath
                IBehaviour[] blockedPathChildren = { new AgentBlocked(), new AgentGenerateSphere(),  new AgentOrbitSphere()};
                blockedPath.childBehaviors = PopulateBranch(blockedPathChildren[0], blockedPathChildren[1], blockedPathChildren[2]);

                //ramPath
                IBehaviour[] ramPathChildren = {new AgentShouldRam(), new AgentRamTarget() };
                ramPath.childBehaviors = PopulateBranch(ramPathChildren[0], ramPathChildren[1]);

                //chasePath
                IBehaviour[] chasePathChildren = { new AgentShouldChase(), new AgentChaseTarget() };
                chasePath.childBehaviors = PopulateBranch(chasePathChildren[0], chasePathChildren[1]);

                //brakePath
                IBehaviour[] brakePathChildren = { new AgentGettingFurther(), new AgentBrake() };
                brakePath.childBehaviors = PopulateBranch(brakePathChildren[0], brakePathChildren[1]);

                //seekPath
                IBehaviour[] seekPathChildren = { new AgentShouldSeek(), new AgentSeekTarget() };
                seekPath.childBehaviors = PopulateBranch(seekPathChildren[0], seekPathChildren[1]);

                //reallignPath
                IBehaviour[] reallignPathChildren = { new AgentShouldReallign(), new AgentReallignToTarget(), new AgentHalt() };
                reallignPath.childBehaviors = PopulateBranch(reallignPathChildren[0], reallignPathChildren[1], reallignPathChildren[2]);

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
