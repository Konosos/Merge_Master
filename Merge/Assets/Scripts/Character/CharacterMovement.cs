using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MergeHero
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private CharController charController;

        [SerializeField] private NavMeshAgent agent;


        private void OnEnable()
        {
            EvenManager.OnGameStarted += TurnOnNavMesh;
        }

        private void OnDisable()
        {
            EvenManager.OnGameStarted -= TurnOnNavMesh;
        }

        private void TurnOnNavMesh()
        {
            agent.enabled = true;
        }
        public void MoveToPosition(Vector3 target)
        {
            if (agent == null)
                return;
            
            agent.SetDestination(target);
            charController.characterAnimation.Run();
        }

        public void MoveIsStopped(bool isStop)
        {
            if (agent == null)
                return;
            agent.isStopped = isStop;
        }

        public NavMeshAgent GetAgent()
        {
            return agent;
        }
    }
}