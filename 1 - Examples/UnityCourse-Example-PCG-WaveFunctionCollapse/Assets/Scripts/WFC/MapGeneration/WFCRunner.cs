using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WFCRunner : MonoBehaviour
{
    private WFCGenerator _generator;
    private WFCAnalyzer _wfcAnalyzer;

    [SerializeField] private TileBase _firstHouse;

    private List<WFCSlot> _startSlots = new List<WFCSlot>();

    // Start is called before the first frame update
    void Start()
    {
        _wfcAnalyzer = GetComponent<WFCAnalyzer>();
        if (_wfcAnalyzer) _wfcAnalyzer.Analyze();

        _generator = GetComponent<WFCGenerator>();
        if (_generator) _generator.Initiate();

        WFCSlot forcedHouseSlot = _generator.Slots[Random.Range(0, _generator.Slots.Count)];
        forcedHouseSlot.ForceTile(_firstHouse);
        _startSlots.Add(forcedHouseSlot);

        StartCoroutine(Run());
        
    }

    private IEnumerator Run()
    {
        if (!_generator)
            yield break;
        
        _generator.Step(_startSlots);
        
        while (_generator.mapGenerated)
        {
            _generator.Step(new List<WFCSlot>());
            yield return new WaitForEndOfFrame();
        }
        
    }
}
