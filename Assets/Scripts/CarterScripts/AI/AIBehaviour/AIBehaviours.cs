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
            Ray ray = new Ray(agent.tr.position, agent.target.transform.position - agent.tr.position);
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
            if (agent.isOrbiting)
            {
                return BehaviourResult.Success;
            }

            float r = agent.hit.collider.transform.localScale.x / 2;
            float s = Random.Range(-180f, 180f);
            float p = agent.spherePadding;

            agent.orbitLocs = new Vector3[(int)Mathf.Ceil(r * 2 / agent.sphereStep)];

            for(int i = 0; i < agent.orbitLocs.Length; i++)
            {
                agent.currStep += agent.sphereStep;
                if (agent.currStep >= r * 2)
                {
                    agent.currStep = 0;
                }
                float t = agent.sphereStep + agent.currStep;
                agent.orbitLocs[i] = new Vector3((r + p) * Mathf.Cos(s) * Mathf.Sin(t), (r + p) * Mathf.Sin(s) * Mathf.Sin(t), (r + p) * Mathf.Cos(t)) + agent.hit.transform.position;
            }

            return BehaviourResult.Success;
        }
    }

    public class AgentOrbitSphere : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.rb.velocity = Vector3.zero;
            agent.isOrbiting = true;

            if (Vector3.Distance(agent.tr.position, agent.orbitLocs[agent.currIndx]) < 1f)
            {
                agent.currIndx++;
                if(agent.currIndx >= agent.orbitLocs.Length)
                {
                    agent.isOrbiting = false;
                    agent.currIndx = 0;
                    return BehaviourResult.Success;
                }
            }

            agent.tr.rotation = Quaternion.RotateTowards(agent.tr.rotation, Quaternion.LookRotation(agent.orbitLocs[agent.currIndx] - agent.tr.position), agent.rotSpeed);
            agent.tr.position += agent.tr.forward * agent.orbitSpeed * Time.deltaTime;
            return BehaviourResult.Success;
        }
    }

    public class AgentResetOrbit : IBehaviour
    {
        public BehaviourResult DoBehaviour(EnemyAI agent)
        {
            agent.isOrbiting = false;
            return BehaviourResult.Success;
        }
    }
}
