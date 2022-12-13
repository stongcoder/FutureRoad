using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public enum RewardType
    {
        Coin,
        Diamond,
        MushRoom,
        Star,
        Weapon,
    }
    public class Reward : SingleTrigger
    {
        public RewardType type;
        public GameObject model;
        public override void DoAction()
        {
            base.DoAction();
            model.SetActive(false);
            levelMgr.GetReward(type);
        }
    }
}

