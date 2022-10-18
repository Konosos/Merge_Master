using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeHero
{
    public class BuyWarriorBtn : MonoBehaviour
    {
        [SerializeField] private Text heroNameTxt;
        [SerializeField] private Text priceTxt;

        private int price;
        private string heroName;

        private void OnEnable()
        {
            EvenManager.OnHeroSpawn += UpdateBtn;
        }

        private void OnDisable()
        {
            EvenManager.OnHeroSpawn -= UpdateBtn;
        }

        private void UpdateBtn()
        {
            CharacterStats characterStats = MatchManager.Instance.CheckHeroMeleeLevelMin();
            if (MatchManager.Instance.CheckHaveCellEmpty() || characterStats == null)
            {
                heroName = GameConfigs.CAPTAIN_NAME;
            }
            else
            {
                heroName = characterStats.charName; 
            }
            int lv = ChessCreater.Instance.GetLevel(heroName);
            int num = MatchManager.Instance.NumHeroOfType(CombatType.Melee);
            int x = (int)Mathf.Pow(2, lv - 1);
            price = 200 * (x * num + x * (x + 1) / 2);
            heroNameTxt.text = heroName;
            priceTxt.text = price.ToString();
        }

        public void SellWarrior()
        {
            if (GameManager.Instance.PlayerMoney < price)
                return;
            GameManager.Instance.AddCoin(-price);
            //MatchManager.Instance.SellWarrior();
            MatchManager.Instance.SellOrImprovedWarrior();
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }
    }
}