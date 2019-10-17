using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviours : MonoBehaviour
{
    public enum BehaviourResult
    {
        Failure,
        Success
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
                if (b.DoBehaviour(agent) == BehaviourResult.Failure)
                    return BehaviourResult.Failure;
            return BehaviourResult.Success;
        }
    }

    public class SelectorNode : IBehaviour
    {
        public List<IBehaviour> childBehaviors = new List<IBehaviour>();

        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            foreach (IBehaviour b in childBehaviors)
                if (b.DoBehaviour(agent) == BehaviourResult.Success)
                    return BehaviourResult.Success;
            return BehaviourResult.Failure;
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
            return BehaviourResult.Success;
        }
    }


    public class AgentShouldSeek : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            if(Vector3.Distance(agent.tr.position, agent.target.transform.position) <= agent.seekDist)
            {
                return BehaviourResult.Success;
            }
            return BehaviourResult.Failure;
        }
    }

    public class AgentShouldChase : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            if (Vector3.Distance(agent.tr.position, agent.target.transform.position) <= agent.chaseDist)
            {
                return BehaviourResult.Success;
            }
            return BehaviourResult.Failure;
        }
    }

    public class AgentShouldRam : IBehaviour
    { 
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            if(Vector3.Distance(agent.tr.position, agent.target.transform.position) <= agent.ramDist)
            {
                return BehaviourResult.Success;
            }
            return BehaviourResult.Failure;
        }
    }

    public class AgentTooFarAway : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            if(agent.maxDist >= Vector3.Distance(agent.tr.position, agent.target.transform.position))
            {
                return BehaviourResult.Success;
            }
            return BehaviourResult.Failure;
        }
    }

    public class AgentShouldReallign : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            if(Vector3.Angle(agent.tr.transform.forward, agent.target.transform.position - agent.tr.position) > 1f)
            {
                return BehaviourResult.Success;
            }
            return BehaviourResult.Failure;
        }
    }


    public class AgentChaseTarget : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.tr.rotation = Quaternion.RotateTowards(agent.tr.rotation, Quaternion.LookRotation(agent.target.transform.position - agent.tr.position), agent.rotSpeed * Time.deltaTime);
            agent.rb.AddForce(agent.tr.forward * agent.chaseSpeed * Time.deltaTime);
            return BehaviourResult.Success;
        }
    }

    public class AgentRamTarget : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.rb.velocity = Vector3.zero;
            agent.rb.velocity = agent.tr.forward * agent.ramSpeed;
            return BehaviourResult.Success;
        }
    }

    public class AgentSeekTarget : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.tr.LookAt(agent.target.transform.position);
            agent.rb.AddForce(agent.tr.forward * agent.seekSpeed * Time.deltaTime);
            return BehaviourResult.Success;
        }
    }

    public class AgentReallignToTarget : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.tr.LookAt(agent.target.transform.position);
            return BehaviourResult.Success;
        }
    }

    public class AgentHalt : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.rb.velocity = Vector3.zero;
            return BehaviourResult.Success;
        }
    }
}
