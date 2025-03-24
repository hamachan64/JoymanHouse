using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField] GameObject characterPanel;
    bool flag = false;

    [SerializeField] GameObject[] character;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI speakingText;

    [SerializeField] GameManager gameManager;

    AvaterManager avater = new AvaterManager();

    void Start()
    {
        characterPanel.SetActive(flag);
        nameText.text = null;
        speakingText.text = "\nV MateÇëIëÇµÇƒÇ≠ÇæÇ≥Ç¢ÅB";
    }

    public void OpenPanel()
    {
        flag = !flag;
        characterPanel.SetActive(flag);
    }

    public void CharacterSelect(GameObject selectChara)
    {
        foreach (GameObject model in character)
        {
            model.SetActive(false);
        }

        selectChara.SetActive(true);
        nameText.text = selectChara.name;
        speakingText.text = null;

        avater = selectChara.GetComponent<AvaterManager>();
        gameManager._avaterManager = avater;
        avater.RunPythonScript(avater.avaterRequest);

        flag = false;
        characterPanel.SetActive(flag);
    }
}
