using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class VoiceCommands : MonoBehaviour
{
    private Dictionary<string, Action> _keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer _keywordRecognizer;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float lightningRadius;
    public LayerMask enemyLayer;
    public float LightningPunchStrength;
    
    void Start()
    {
        // I have scrapped the idea of using mouth sounds similar to the real phenomena, probably wont work
        _keywordActions.Add("Thunder", Lightning);
        _keywordActions.Add("Water", Water);
        _keywordActions.Add("Air", Air);

        _keywordRecognizer = new KeywordRecognizer(_keywordActions.Keys.ToArray()); //, confidence);
        _keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        _keywordRecognizer.Start();
        
        // Some debugging to make sure correct mic is running
        Debug.Log( _keywordRecognizer.IsRunning );
        var mics = Microphone.devices;
        foreach (var mic in mics)
        {
            Debug.Log(mic);
        }
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        _keywordActions[args.text].Invoke();
    }

    private void Lightning()
    {
        Debug.Log("Lightning triggered");
        GameObject enemy = FindNearestEnemy();
        Vector3 direction = enemy.transform.position - transform.position;
        // Add force to the enemy, roughly a 20 degree arc from the ground
        enemy.GetComponent<Rigidbody>().AddForce(direction.normalized * LightningPunchStrength + Vector3.up * LightningPunchStrength / 5, ForceMode.Impulse);
    }
    private void Water()
    {
        Debug.Log("Water triggered");
    }
    private void Air()
    {
        Debug.Log("Air triggered");
    }

    // Finds and returns the nearest enemy within 
    private GameObject FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, lightningRadius, enemyLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // Enemy found
                Debug.Log("I'm going to attack " + hit.name);
                return hit.gameObject;
                break;  // Only attack one?
            }
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, lightningRadius);
    }
}
