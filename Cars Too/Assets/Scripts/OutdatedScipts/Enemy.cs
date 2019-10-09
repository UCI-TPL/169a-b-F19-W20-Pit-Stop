using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private GameObject player;

    [SerializeField] private BaseStats baseStats;

    [SerializeField] private UnityEngine.AI.NavMeshAgent navMeshAgent; // This is a reference to the navmesh agent component that is attached to the enemy 

    void Start()
    {
        stats = new Stats();
        stats.InitializeStats(baseStats);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerLocation = player.transform.position;

        navMeshAgent.SetDestination(playerLocation);
    }
}
