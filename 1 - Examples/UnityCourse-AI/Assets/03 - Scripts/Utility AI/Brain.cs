using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI_
{
    public enum AIFaction
    {
        Ghost,
        Others
    }
    
    public class Brain : MonoBehaviour
    {

        [SerializeField] private Context _context;
        [SerializeField] private List<UtilityAction> _actions = new List<UtilityAction>();
        [SerializeField] private AIFaction _faction;

        public AIFaction Faction => _faction;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach (var action in _actions)
            {
                action.Initialize(_context);
            }
        }

        // Update is called once per frame
        void Update()
        {
            float bestScore = float.MinValue;
            UtilityAction bestUtilityAction = null;

            foreach (var action in _actions)
            {
                float score = action.Evaluate(_context);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestUtilityAction = action;
                }
            }
            
            bestUtilityAction?.Execute(_context);
            if(bestUtilityAction != null)
            {
                Debug.Log($"Best Action = {bestUtilityAction.name}");
            }
            else
            {
                Debug.Log($"No Action");
            }

        }
    }
}
