using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float speed;
    [SerializeField] private bool isStartTalk = false;
    private int index;
    [SerializeField] private string tableLanguageName;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MusicManager.Instance.PlaySFX("Click");
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(LoadLocalizedLines());
    }

    private IEnumerator LoadLocalizedLines()
    {
        yield return LocalizationSettings.InitializationOperation;
        var localizedStringTable = LocalizationSettings.StringDatabase.GetTable(tableLanguageName);

        if (localizedStringTable != null)
        {
            lines = new string[localizedStringTable.Count];
            Debug.Log(localizedStringTable.Count);
            for (int i = 0; i < localizedStringTable.Count; i++)
            {
                var entry = localizedStringTable.GetEntry(i.ToString());
                if (entry != null)
                {
                    lines[i] = entry.GetLocalizedString();
                }
                else
                {
                    Debug.LogWarning($"Entry with key {i} not found in DialogueTable.");
                }
            }
        }

        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            if (isStartTalk)
            {
                SceneManager.LoadScene("Scene1");
            }
            
        }
    }
}
