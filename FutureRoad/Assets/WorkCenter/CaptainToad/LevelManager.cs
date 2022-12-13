using MechanicControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CaptainToad;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerController player;
    public ProceduralBlockCreater creater => creaters[createrIndex];
    public int createrIndex = 0;
    public List<ProceduralBlockCreater> creaters;
    public GameObject previewObj;
    private static LevelManager _Instance;
    public InteractTip interactTip;
    public Text goldText;
    public Text diamondText;
    public Text mushroomText;
    public Image winImg;
    private int _goldNum;
    public float weaponTime = 10f;
    private bool isAttacking;
    public AudioSource audioSource;
    public AudioClip attackMusic;
    public AudioClip normalMusic;
    public AudioClip successMusic;
    public float pauseTime;
    public int goldNum
    {
        get
        {
            return _goldNum;
        }
        set
        {
            _goldNum = value;
            goldText.text = _goldNum.ToString();
        }
    }
    private int _diamondNum;
    public int diamondNum
    {
        get
        {
            return _diamondNum;
        }
        set
        {
            _diamondNum = value;
            diamondText.text = _diamondNum.ToString();
        }
    }
    private int _mushroomNum;
    public int mushroomNum
    {
        get
        {
            return _mushroomNum;
        }
        set
        {
            _mushroomNum = value;
            mushroomText.text=_mushroomNum.ToString();
        }
    }
    public static LevelManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<LevelManager>();
            }
            return _Instance;
        }
    }
#if UNITY_EDITOR
    [ContextMenu("´ò¿ª´°¿Ú")]
    public void OpenEditor()
    {
        LevelManagerEditor.Open(this);
    }
#endif
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void ProcessTrigger(Collider other, InteractType type)
    {
        if (other.gameObject.CompareTag("Mechanic"))
        {
            var p = other.transform;
            var dm = p.GetComponentInParent<LevelTriggerBase>();
            if (dm != null)
            {
                switch (type)
                {
                    case InteractType.Enter:
                        dm.TriggerIn(other);
                        break;
                    case InteractType.Exit:
                        dm.TriggerOut(other);
                        break;
                }
            }
        }
    }
    public void StartAttack(float time)
    {
        player.StartAttack();
        isAttacking = true;
        pauseTime = audioSource.time;
        audioSource.clip = attackMusic;
        audioSource.Play();
        StartCoroutine(StopAttack(time));
    }
    IEnumerator StopAttack(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.clip = normalMusic;
        audioSource.time = pauseTime;
        audioSource.Play();
        player.StopAttack();
        isAttacking = false;
    }
    public void GetReward(RewardType type)
    {
        switch (type)
        {
            case RewardType.Coin:
                {
                    goldNum += 1;
                }
                break;
            case RewardType.Diamond:
                {
                    diamondNum += 1;
                }
                break;
            case RewardType.MushRoom:
                {
                    mushroomNum += 1;
                }
                break;
            case RewardType.Star:
                {
                    winImg.gameObject.SetActive(true);
                    audioSource.clip = successMusic;
                    audioSource.time = 0;
                    audioSource.Play();
                    audioSource.loop = false;
                    break;
                }
            case RewardType.Weapon:
                {
                    StartAttack(weaponTime);
                    break;
                }
        }
    }
    public void ShowHandleTips(Action cb)
    {
        if (isAttacking) return;
        interactTip.interactCb = cb;
        interactTip.gameObject.SetActive(true);
    }
    public void CloseHandleTips()
    {
        interactTip.gameObject.SetActive(false);
    }
}
public enum InteractType
{
    none,
    Enter,
    Exit,
}