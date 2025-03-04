using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public TMP_InputField nameInputField; 
    public TextMeshProUGUI displayNameText; 

    private void Start()
    {
        string savedName = PlayerPrefs.GetString("PlayerName", "Han Solo");
        nameInputField.text = savedName;
        displayNameText.text = savedName;

        nameInputField.onValueChanged.AddListener(UpdatePlayerName);
    }



    private void UpdatePlayerName(string newName)
    {
        PlayerPrefs.SetString("PlayerName", newName);
        PlayerPrefs.Save();

        displayNameText.text = newName;
    }

}
