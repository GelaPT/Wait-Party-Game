using UnityEngine;

public static class JsonTools
{
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    public static Dialogue[] GetDialogues() => FromJson<Dialogue>(Resources.Load<TextAsset>("Json/dialogues").text);

    public static Minigame[] GetMinigames() => FromJson<Minigame>(Resources.Load<TextAsset>("Json/minigames").text);

    public static T[] FromJson<T>(string json) => JsonUtility.FromJson<Wrapper<T>>(json).Items;

    public static string ToJson<T>(T[] array, bool pretty = true) => JsonUtility.ToJson(new Wrapper<T> { Items = array }, pretty);
}