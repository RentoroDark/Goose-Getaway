using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialCanvas : MonoBehaviour
{
    [SerializeField] GameObject _exitTutorial;
    [SerializeField] Image _moveImage;
    [SerializeField] GameObject _moveTutorial;
    private bool _moved;
    [SerializeField] Image _jumpImage;
    [SerializeField] GameObject _jumpTutorial;
    [SerializeField] TextMeshProUGUI jumpText;
    private bool _jumped;
    [SerializeField] List<GameObject> _tutorialObjects;
    [SerializeField] List<GameObject> _exitTutorials;



    public async void MoveTutorial()
    {
        float i = 0;
        _moved = false;
        Time.timeScale = 0;
        _moveTutorial.SetActive(true);
        while (true)
        {
            _moveImage.transform.position += new Vector3(Mathf.Sin(i), 0, 0);
            if (_moved)
            {
                break;
            }
            i += 0.1f;
            await Awaitable.EndOfFrameAsync();
        }
        _moveTutorial.SetActive(false);
        Time.timeScale = 1;
    }

    public async void JumpTutorial()
    {
        float i = 0;
        _jumped = false;
        Time.timeScale = 0;
        _jumpTutorial.SetActive(true);
        while (true)
        {
            _jumpImage.transform.localScale = new Vector3(0.5f + Mathf.Sin(i) * 0.05f , 0.5f + Mathf.Sin(i) * 0.05f , 0);
            if (_jumped)
            {
                break;
            }
            i += 0.1f;
            await Awaitable.EndOfFrameAsync();
        }
        Time.timeScale = 1;
    }
    public async void SlamTutorial()
    {
        float i = 0;
        _jumpImage.transform.localScale = Vector3.one;
        _jumped = false;
        Time.timeScale = 0;
        jumpText.text = "Нажмите еще раз пока вы в воздухе чтобы приземлится быстрее";
        _jumpTutorial.SetActive(true);
        while (true)
        {
            _jumpImage.transform.localScale = new Vector3(0.5f + Mathf.Sin(i) * 0.05f , 0.5f + Mathf.Sin(i) * 0.05f, 0);
            if (_jumped)
            {
                break;
            }
            i += 0.1f;
            await Awaitable.EndOfFrameAsync();
        }
        _jumpTutorial.SetActive(false);
        Time.timeScale = 1;
        EndTutorial();
    }
    public async void ExitTutorial()
    {
        Time.timeScale = 0;
        _exitTutorial.SetActive(true);
    }

    public async void CloseExitTutorial()
    {
        _exitTutorial.SetActive(false);
        Time.timeScale = 1;
        foreach (var obj in _exitTutorials)
        {
            obj.SetActive(false);
        }
    }

    async void EndTutorial()
    {
        await Awaitable.WaitForSecondsAsync(2);
        foreach (var a in _tutorialObjects)
        {
            Time.timeScale = 1;
            a.SetActive(false);
            
        }
    }

    public void OnJump(InputAction.CallbackContext callback)
    {
        _jumped = true;
    }
    
    public void OnMoved(InputAction.CallbackContext callback)
    {
        _moved = true;
    }
}
