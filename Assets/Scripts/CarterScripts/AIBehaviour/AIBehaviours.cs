using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviours : MonoBehaviour
{
    public enum BehaviourResult
    {
        failure,
        success
    }

    public interface IBehaviour
    {
        BehaviourResult DoBehaviour(EnemyAI agent);
    }

    public class SequenceNode : IBehaviour
    {
        public List<IBehaviour> childBehaviors = new List<IBehaviour>();

        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            foreach (IBehaviour b in childBehaviors)
                if (b.DoBehaviour(agent) == BehaviourResult.failure)
                    return BehaviourResult.failure;
            return BehaviourResult.success;
        }
    }

    public class SelectorNode : IBehaviour
    {
        public List<IBehaviour> childBehaviors = new List<IBehaviour>();

        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            foreach (IBehaviour b in childBehaviors)
                if (b.DoBehaviour(agent) == BehaviourResult.success)
                    return BehaviourResult.success;
            return BehaviourResult.failure;
        }
    }

    public class DebugNode : IBehaviour
    {
        string text;

        public DebugNode(string text)
        {
            this.text = text;
        }

        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            Debug.Log(text);
            return BehaviourResult.success;
        }
    }





    public class AgentChaseTarget : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, agent.target.transform.rotation, agent.rotSpeed * Time.deltaTime);
            agent.rb.AddForce(agent.transform.forward * agent.speed * Time.deltaTime);
            return BehaviourResult.success;
        }
    }

}
