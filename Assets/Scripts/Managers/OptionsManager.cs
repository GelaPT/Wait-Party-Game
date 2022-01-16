using UnityEngine;

public enum AudioType { SFX, Music };

public class OptionsManager : Singleton<OptionsManager>
{
    [HideInInspector] public GameOptions gameOptions;
    public bool[] allowedResolutions = new bool[(int)Resolutions.W800x450 + 1];

    private void Start()
    {
        gameOptions = SaveManager.LoadData<GameOptions>();

        if (gameOptions == null)
        {
            gameOptions = new GameOptions(Screen.resolutions);
            SaveManager.SaveData(gameOptions);
        }


        for (int i = (int)gameOptions.resolution - 1; i >= 0; i--) {
            allowedResolutions[i] = false;
        }

        ChangeResolution(gameOptions.resolution);
    }

    public void ChangeMusicVolume(float volume)
    {
        gameOptions.musicVolume = volume;
        /*foreach (AudioSource audio in AudioManager.Instance.Music)
        {
            audio.volume = volume;
        }*/
    }

    public void ChangeSFXVolume(float volume)
    {
        gameOptions.sfxVolume = volume;
        /*foreach (AudioSource audio in AudioManager.Instance.Sfx)
        {
            audio.volume = volume;
        }*/
    }

    public void ChangeFullscreen(bool fullscreen)
    {
        gameOptions.fullscreen = fullscreen;

        Screen.fullScreenMode = gameOptions.fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void ChangeResolution(Resolutions resolution)
    {
        gameOptions.resolution = resolution;

        switch (gameOptions.resolution)
        {
            case Resolutions.W800x450:
                Screen.SetResolution(800, 450, Screen.fullScreenMode);
                break;
            case Resolutions.W1024xH576:
                Screen.SetResolution(1024, 576, Screen.fullScreenMode);
                break;
            case Resolutions.W1280xH720:
                Screen.SetResolution(1280, 720, Screen.fullScreenMode);
                break;
            case Resolutions.W1366xH768:
                Screen.SetResolution(1336, 768, Screen.fullScreenMode);
                break;
            case Resolutions.W1600xH900:
                Screen.SetResolution(1600, 900, Screen.fullScreenMode);
                break;
            case Resolutions.W1920xH1080:
                Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
                break;
            default:
                Screen.SetResolution(800, 450, Screen.fullScreenMode);
                break;
        }
    }
}
