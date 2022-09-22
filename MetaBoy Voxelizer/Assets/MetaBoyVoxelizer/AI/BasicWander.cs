using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicWander : MonoBehaviour
{
    NavMeshAgent Agent;
    Animator AnimationController;
    BodyComponent Body;
    public float DestinationThreshold = 10.0f;
    bool isPathing = false;
    public bool WillReachDestination
    {
        get
        {
            if(!Agent)
            {
                return false;
            }
            return Agent.remainingDistance < DestinationThreshold;
        }
    }
    void Start()
    {
        AnimationController = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Body = GetComponentInChildren<BodyComponent>();
        float RandomX = Random.Range(-155.0f, -245.0f);
        float RandomZ = Random.Range(55.0f, 145.0f);
        Agent.destination = new Vector3(RandomX, 1.0f, RandomZ);
        isPathing = true;
    }

    private void Update()
    {
        if(isPathing)
        {
            if(WillReachDestination)
            {
                isPathing = false;
                Agent.isStopped = true;
                float invokeTimer = Random.Range(0.6f, 3.5f);
                Invoke("DecideAction", invokeTimer);
            }
        }

        if (AnimationController && Agent)
        {
            float speedValue = Agent.velocity.magnitude / Agent.speed;
            AnimationController.SetFloat("Speed", speedValue);
        }
    }

    void DecideAction()
    {
        int Randomizer = Random.Range(-2, 10);
        if(Randomizer < 4)
        {
            Wander();
        }
        else if(Randomizer < 6)
        {
            if (Body && Body.WeaponAnimTrigger != "" && AnimationController)
            {
                AnimationController.SetTrigger(Body.WeaponAnimTrigger);
                Invoke("DecideAction", 4.0f);
            }
            else
            {
                AnimationController.SetTrigger("AttackPunch");
                Invoke("DecideAction", 4.0f);
            }
        }
        else if(Randomizer < 9)
        {
            AnimationController.SetTrigger("DoDance");
            Invoke("DecideAction", 4.0f);
        }
        else
        {
            AnimationController.SetTrigger("DoVictory");
            Invoke("DecideAction", 4.0f);
        }
    }

    void Wander()
    {
        float RandomX = Random.Range(-155.0f, -245.0f);
        float RandomZ = Random.Range(55.0f, 145.0f);
        Agent.destination = new Vector3(RandomX, 1.0f, RandomZ);
        Agent.isStopped = false;
        isPathing = true;
    }
}
