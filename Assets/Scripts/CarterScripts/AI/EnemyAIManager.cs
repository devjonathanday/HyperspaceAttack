using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static AIBehaviours;

public class EnemyAIManager : MonoBehaviour
{
    public GameManager gm;
    public GameObject chaserPrefab;
    public float enemySpawnRadius;

    public int maxEnemies;
    public int maxEnemiesPerDifficulty;
    public int currActiveEnemies;



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

    private EnemyAI GenerateEnemy()
    {
        var g = Instantiate<GameObject>(chaserPrefab, Random.onUnitSphere * (gm.arenaRadius + enemySpawnRadius), Quaternion.identity);
        var agent = g.GetComponent<EnemyAI>();
        agent.gm = gm;
        agent.ID = (uint)Random.Range(1, 10000);
        agent.target = gm.playerBody;
        return agent;
    }

    private void Start()
    {
        chaserBehaviour = PopulateBehaviours(EnemyAI.Type.Chaser);
        for(int i = 0; i < maxEnemies; i++)
        {
            agents.Add(GenerateEnemy());
            agents[i].gameObject.SetActive(false);
        }
    }


    void Update()
    {
        currActiveEnemies = 0;
        foreach(EnemyAI agent in agents)
        {
            if (agent.gameObject.activeInHierarchy)
            {
                currActiveEnemies++;
            }
        }

        int iterator = 0;
        while(currActiveEnemies < (int)(maxEnemiesPerDifficulty * gm.difficulty))
        {
            if (!agents[iterator].gameObject.activeInHierarchy)
            {
                agents[iterator].transform.position = Random.onUnitSphere * (gm.arenaRadius + enemySpawnRadius);
                agents[iterator].gameObject.SetActive(true);
                currActiveEnemies++;
                iterator++;
            }
        }

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
