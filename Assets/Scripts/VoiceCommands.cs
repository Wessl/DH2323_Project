using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.VFX;
using UnityEngine.Windows.Speech;

public class VoiceCommands : MonoBehaviour
{
    private Dictionary<string, Action> _keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer _keywordRecognizer;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float lightningRadius;
    public LayerMask enemyLayer;
    public float lightningPunchStrength;
    public VisualEffect lightningFX;
    public ParticleSystem windFX;
    private float _lightningScaler;
    private int _lightningDistanceToEnemyPropertyID;
    public float lightningImpactDelay;

    public string lightningKeyword;
    public string waterKeyword;
    public string windKeyword;

    public bool limitLightningToOneEnemy;

    void Start()
    {
        // I have scrapped the idea of using mouth sounds similar to the real phenomena, probably wont work
        _keywordActions.Add(lightningKeyword, Lightning);
        _keywordActions.Add(waterKeyword, Water);
        _keywordActions.Add(windKeyword, Wind);

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
        
        // Misc
        _lightningScaler = 6;        // This is the scale of the lightning strike in the Z direction, use to scale down position values
        _lightningDistanceToEnemyPropertyID = Shader.PropertyToID("DistanceToEnemy");
        
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        _keywordActions[args.text].Invoke();
    }

    private void Lightning()
    {
        Debug.Log("Lightning triggered");
        List<GameObject> enemies = FindEnemiesByDist();
        foreach (var enemy in enemies)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            // Instantiate lightning strike
            var lightning = Instantiate(lightningFX, transform.position, Quaternion.identity);
            var lightningTransform = lightning.transform;
            lightningTransform.rotation = Quaternion.LookRotation(direction, transform.up);
            lightning.SetFloat(_lightningDistanceToEnemyPropertyID, direction.magnitude);
            StartCoroutine(LightningPushForce(enemy, direction));

            if (limitLightningToOneEnemy)
            {
                break;
            }
        }
    }

    IEnumerator LightningPushForce(GameObject enemy, Vector3 direction)
    {
        yield return new WaitForSeconds(lightningImpactDelay);
        // Add force to the enemy, roughly a 20 degree arc from the ground
        enemy.GetComponent<Rigidbody>().AddForce(direction.normalized * lightningPunchStrength + Vector3.up * lightningPunchStrength / 5, ForceMode.Impulse);
    }
    private void Water()
    {
        Debug.Log("Water triggered");
    }
    private void Wind()
    {
        Debug.Log("Air triggered");
        Instantiate(windFX, transform.position, transform.rotation);
    }

    // Finds and returns the nearest enemy within 
    List<GameObject> FindEnemiesByDist()
    {
        var myPos = transform.position;
        Collider[] hits = Physics.OverlapSphere(myPos, lightningRadius, enemyLayer);
        List<GameObject> enemiesHit = new List<GameObject>();
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                enemiesHit.Add(hit.gameObject);
            }
        }
        // Sort by distance
        enemiesHit.Sort((a,b) => GetDist(a,myPos).CompareTo(GetDist(b,myPos)));
        return enemiesHit;
    }

    // This needs to be BLAZING FAST
    private float GetDist(GameObject a, Vector3 pos)
    {
        return (a.transform.position - pos).magnitude;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, lightningRadius);
    }
}
