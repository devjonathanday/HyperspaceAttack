using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviours : MonoBehaviour //Implementation
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

    //Special Nodes
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

    //End Special Nodes

    //Question Nodes
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
            if(Vector3.Distance(agent.tr.position, agent.target.transform.position) >= agent.maxDist)
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
            if(Vector3.Angle(agent.tr.transform.forward, agent.target.transform.position - agent.tr.position) > agent.allignDegree)
            {
                return BehaviourResult.Success;
            }
            return BehaviourResult.Failure;
        }
    }

    public class AgentGettingFurther : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            float t = Vector3.Distance(agent.tr.position, agent.target.transform.position);

            if(agent.distToTarget == null)
            {
                agent.distToTarget = t;
                return BehaviourResult.Failure;
            }

            if(t > agent.distToTarget)
            {
                agent.distToTarget = t;
                return BehaviourResult.Success;
            }
            else
            {
                return BehaviourResult.Failure;
            }
        }
    }

    public class AgentBlocked : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            Ray ray = new Ray(agent.tr.position, agent.target.transform.position);
            if (Physics.Raycast(ray, out agent.hit, agent.blockDist, 1 << 13))
            {
                return BehaviourResult.Success;
            }
            return BehaviourResult.Failure;
        }
    }

    //End Question Nodes

    //Action Nodes

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

    public class AgentBrake : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.rb.velocity *= 1 - agent.brakeMultiplier;
            return BehaviourResult.Success;
        }
    }

    public class AgentGenerateSphere : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            float r = agent.hit.collider.transform.localScale.x;
            float s = Vector3.Angle(agent.tr.up, Vector3.up);
            float t;

            agent.sphereLocs = new Vector3[(int)(360 / agent.sphereStep)];
            for(int i = 0; i < agent.sphereLocs.Length; i++)
            {
                t = agent.sphereStep * (i + 1);
                agent.sphereLocs[i] = new Vector3(r * Mathf.Cos(s) * Mathf.Sin(t), r * Mathf.Sin(s) * Mathf.Sin(t), r * Mathf.Cos(t));
            }
            return BehaviourResult.Success;
        }
    }
}
