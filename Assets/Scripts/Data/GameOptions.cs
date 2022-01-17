using UnityEngine;

[System.Serializable]
public enum Resolutions : int
{
    W1920xH1080 = 0,
    W1600xH900 = 1,
    W1366xH768 = 2,
    W1280xH720 = 3,
    W1024xH576 = 4,
    W800x450 = 5
}

[System.Serializable]
public class GameOptions
{
    public Resolutions resolution;
    public bool fullscreen;
    public float musicVolume;
    public float sfxVolume;

    public GameOptions(Resolution[] resolutions)
    {
        if ( resolutions[^1].width < 1024 || resolutions[^1].height < 576) resolution = Resolutions.W800x450;
        else if ( resolutions[^1].width < 1280 || resolutions[^1].height < 720 ) resolution = Resolutions.W1024xH576;
        else if ( resolutions[^1].width < 1336 || resolutions[^1].height < 768 ) resolution = Resolutions.W1280xH720;
        else if ( resolutions[^1].width < 1600 || resolutions[^1].height < 900 ) resolution = Resolutions.W1366xH768;
        else if ( resolutions[^1].width < 1920 || resolutions[^1].height < 1080 ) resolution = Resolutions.W1600xH900;
        else resolution = Resolutions.W1920xH1080;

        musicVolume = 0.5f;
        sfxVolume = 0.5f;

        fullscreen = true;
    }

    public override string ToString()
    {
        return "resolution: " + resolution + "| fullscreen: " + fullscreen + "| music: " + musicVolume + "| sfx: " + sfxVolume;
    }
}
