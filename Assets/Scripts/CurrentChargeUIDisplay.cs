using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CurrentChargeUIDisplay : MonoBehaviour
{
    public PyroCasting pyroCasting;

    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, pyroCasting.CurrentPyroCharge);
    }
}
