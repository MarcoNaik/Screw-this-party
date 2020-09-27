using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public float transitionSpeed = 1f;

    public PostProcessVolume volume;

    private Bloom _bloom;

    private void Start()
    {
        volume.profile.TryGetSettings(out _bloom);
        _bloom.intensity.value = 0;
        _bloom.threshold.value = 1;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ReloadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    

    IEnumerator LoadLevel(int levelIndex)
    {
        while (_bloom.intensity.value < 8 && _bloom.threshold.value > 0.1f)
        {
            _bloom.intensity.value += transitionSpeed* 0.8f;
            _bloom.threshold.value -= transitionSpeed* 0.09f;
            yield return null;
        }
        SceneManager.LoadScene(levelIndex);
        
        while (_bloom.intensity.value > 1 && _bloom.threshold.value < 1f)
        {
            _bloom.intensity.value -= transitionSpeed* 0.8f;
            _bloom.threshold.value += transitionSpeed* 0.09f;
            yield return null;
        }

    }
}
