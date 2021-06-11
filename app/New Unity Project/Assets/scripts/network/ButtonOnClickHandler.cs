using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClickHandler : MonoBehaviour
{
    public Button LightSwitch;
    public client client;

    // Start is called before the first frame update
    void Start()
    {
        LightSwitch.onClick.AddListener(LightSwitchOnClick);
    }

    void LightSwitchOnClick(){
        client.SendMessage("Lights@On\n");
    }
}
