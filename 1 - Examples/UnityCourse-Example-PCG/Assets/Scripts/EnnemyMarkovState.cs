using UnityEngine;


    [CreateAssetMenu(fileName = "Ennemy", menuName = "Dungeon Crawler/Ennemy Stereotype", order = 0)]
    public class EnnemyMarkovState : ScriptableObject
    {
        [Header("Menace")]
        [SerializeField] private int _menacePoint;
        
        [Header("Stats")]
        [SerializeField] private float _atkRate;
        [SerializeField] private float _defRate;
        [SerializeField] private float _hpMax;
        
        [Header("Prefab")]
        [SerializeField] private GameObject _prefab;
        
    }