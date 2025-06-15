using System;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.MoneyManager
{
    public class MoneyManager : MonoSingleton<MoneyManager>
    {
        [SerializeField] private int money;
        private const string GoldKey = "Money";
        
        //다른 곳에서 돈 변수를 써야할 때 MoneyManager.money 사용 가능(얘 int형임))
        public int Money => PlayerPrefs.GetInt(GoldKey, money);

        //Money가 바뀌었을 때 모든 구독자들에게 방송하는 시스템
        public event Action<int> OnMoneyChanged;

        private void Start()
        {
            if (Instance == this)
                DontDestroyOnLoad(this);
        }

        //돈 추가할 때 (어디서든 MoneyManager.Instance.AddMoney(돈 추가값(int형))로 사용 가능)
        public void AddMoney(int amount)
        {
            //돈 += 돈 추가값(int)
            money += amount;
            PlayerPrefs.SetInt(GoldKey, money);
            //구독자가 null이 아니라면(구독자가 이벤트 듣고 있으면), 실행해(돈이 바뀜)
            OnMoneyChanged?.Invoke(money);
        }

        //돈 추가할 때 (어디서든 MoneyManager.Instance.SpendMoney(돈 감소값(int형))로 사용 가능)
        public void SpendMoney(int amount)
        {
            //돈 += 돈 추가값(int)
            money -= amount;
            PlayerPrefs.SetInt(GoldKey, money);
            //구독자가 null이 아니라면(구독자가 이벤트 듣고 있으면), 실행해(돈이 바뀜, (방송을 한다))
            OnMoneyChanged?.Invoke(money);
        }

        //돈 추가할 때 (어디서든 MoneyManager.Instance.SetMoney(돈 설정값(int형))로 사용 가능)
        public void SetMoney(int value)
        {
            //돈 = 돈 설정값(int)
            money = value;
            PlayerPrefs.SetInt(GoldKey, money);
            //구독자가 null이 아니라면(구독자가 이벤트 듣고 있으면), 실행해(돈이 설정됨, (방송을 한다))
            OnMoneyChanged?.Invoke(money);
        }
    }
}
