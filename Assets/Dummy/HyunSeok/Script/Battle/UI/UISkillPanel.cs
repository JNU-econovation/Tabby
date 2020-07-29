using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

namespace Battle
{
    public class UISkillPanel : MonoBehaviour
    {
        public List<Button> buttons;
        public List<Image> banImages;
        public List<Sprite> icons;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            SetSkillButton();
        }

        void SetSkillButton()
        {
            for (int i = 0; i < 3; i++)
            {
                if (DataManager._instance.gogoAnimalIndexes[i] == -1)
                {
                    buttons[i].gameObject.SetActive(false);
                }
                else
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].transform.GetChild(0).GetComponent<Text>().text = AnimalManager._instance.animals[i].skillDatas[0].skillName;
                    buttons[i].image.sprite = icons[AnimalManager._instance.animals[i].animalData.Index];
                }
            }
        }

        public void OnClickSkill(int index)
        {
            if (AnimalManager._instance.animals[index].OnClickSkill())
            {
                banImages[index].gameObject.SetActive(true);
                buttons[index].enabled = false;
                StartCoroutine(CoolTimeCoroutine(index));
            }
        }

        IEnumerator CoolTimeCoroutine(int index)
        {
            Image filledImage = banImages[index].transform.GetChild(0).GetComponent<Image>();
            float maxCoolTime = AnimalManager._instance.animals[index].skillDatas[0].coolTime;
            float time = maxCoolTime;
            while (time > 0f)
            {
                time -= Time.deltaTime;
                filledImage.fillAmount = time / maxCoolTime;
                yield return null;
            }
            banImages[index].gameObject.SetActive(false);
            if (AnimalManager._instance.animals[index].animalData.HP > 0)
                buttons[index].enabled = true;
        }
    }

}
