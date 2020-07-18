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
                }
            }
        }

        public void OnClickSkill(int index)
        {
            AnimalManager._instance.animals[index].OnClickSkill();
        }
    }

}
