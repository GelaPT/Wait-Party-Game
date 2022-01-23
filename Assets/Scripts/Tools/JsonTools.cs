using UnityEngine;

public static class JsonTools
{
    public static Dialogue[] GetDialogues() => JsonUtility.FromJson<Dialogue[]>(Resources.Load<TextAsset>("Json/dialogues.json").text);
}
