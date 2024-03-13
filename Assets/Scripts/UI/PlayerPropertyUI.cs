using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPropertyUI : MonoBehaviour
{
    private GameObject uiGameObject;
    // 两个 progressbar；一个 Grid；一个 icon
    private Image levelProgressBar;
    private TextMeshProUGUI levelText;

    private Image HPProgressBar;
    private TextMeshProUGUI HPText;

    private GameObject propertyGrid;

    private Image weaponIcon;

    public GameObject propertyPrefab;

    private PlayerProperty playerProperty;
    private PlayerAttack playerAttack;

    public static PlayerPropertyUI Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); return;
        }
        Instance = this;
    }
    private void Start()
    {
        uiGameObject = transform.Find("UI").gameObject;
        levelProgressBar = transform.Find("UI/LevelProgressBar/ProgressBar").GetComponent<Image>();
        levelText = transform.Find("UI/LevelProgressBar/LevelText").GetComponent<TextMeshProUGUI>();

        HPProgressBar = transform.Find("UI/HPProgressBar/ProgressBar").GetComponent<Image>();
        HPText = transform.Find("UI/HPProgressBar/HPText").GetComponent<TextMeshProUGUI>();

        propertyGrid = transform.Find("UI/PropertyGrid").gameObject;

        weaponIcon = transform.Find("UI/WeaponIcon").GetComponent<Image>();

        weaponIcon.enabled = false;

        GameObject player = GameObject.FindWithTag(Tags.PLAYER);
        playerProperty = player.GetComponent<PlayerProperty>();
        playerAttack = player.GetComponent<PlayerAttack>();
        UpdatePlayerPropertyUI();
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            uiGameObject.SetActive(uiGameObject.activeSelf ? false : true);
        }
        
    }
    public void UpdatePlayerPropertyUI()
    {
        levelProgressBar.fillAmount = playerProperty.currenExp * 1.0f / (playerProperty.level * 30);
        levelText.text = playerProperty.level.ToString();

        HPProgressBar.fillAmount = playerProperty.HPValue * 1.0f / 100;
        HPText.text = playerProperty.HPValue.ToString() + "/" + "100";

        ClearGrid();

        foreach (var item in playerProperty.propertyDict)
        {
            string text = "";
            switch(item.Key)
            {
                case PropertyType.HPValue:
                    continue;
                case PropertyType.EnergyValue:
                    text += "能量"; break;
                case PropertyType.MentalValue:
                    text += "精神"; break;
                case PropertyType.SpeedValue:
                    text += "速度"; break;
                case PropertyType.AttackValue:
                    text += "攻击"; break;
            }
            int sum = 0;
            foreach(Property it in item.Value)
            {
                sum += it.value;
            }
            text += sum;
            AddProperty(text);
        }

        if (playerAttack.weapon != null)
        {
            weaponIcon.enabled = true;
            weaponIcon.sprite = playerAttack.weaponIcon;
        } else
        {
            weaponIcon.enabled = false;
        }
    }

    private void ClearGrid()
    {
        foreach(Transform property in propertyGrid.transform)
        {
            Destroy(property.gameObject);
        }
    }

    private void AddProperty(string text)
    {
        GameObject go = GameObject.Instantiate(propertyPrefab);
        go.SetActive(true);
        go.transform.SetParent(propertyGrid.transform);
        go.transform.Find("PropertyText").GetComponent<TextMeshProUGUI>().text = text;
    }

    public void Show()
    {
        uiGameObject.SetActive(true);
    }
    public void Hide()
    {
        uiGameObject.SetActive(false);
    }
}
