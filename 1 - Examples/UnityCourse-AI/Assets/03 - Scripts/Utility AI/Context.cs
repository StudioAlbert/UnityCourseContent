using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace UtilityAI_
{
    public class Context : MonoBehaviour
    {

        private CryptPercept[]_crypts;
        public List<Transform> GetCrypts() => _crypts.Select(p  => p.Transform).ToList();
        
        private PlayerPercept _player;
        public Transform GetPlayerTransform() => _player.Transform;
        
        //private NavMeshAgent _agent;
        public Transform SelfTransform => transform;
        public void MoveTo(Vector3 position)
        {
            return;
        }

        private Transform[] _friends;
        public Transform[] GetFriends() => _friends;
        

        private void OnEnable()
        {
            _crypts = FindObjectsByType<CryptPercept>(FindObjectsSortMode.None);
            _player = FindFirstObjectByType<PlayerPercept>();

            Brain myBrain = GetComponent<Brain>();
            _friends = FindObjectsByType<Brain>(FindObjectsSortMode.None)
                .Where(b => b.Faction == myBrain.Faction && b != myBrain)
                .Select(b => b.transform)
                .ToArray();

        }

    }
}