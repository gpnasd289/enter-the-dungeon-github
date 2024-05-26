using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionInfo : MonoBehaviour
{
    private TextMeshProUGUI versionTxt;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        versionTxt = GetComponent<TextMeshProUGUI>();
        versionTxt.text = "v" + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
