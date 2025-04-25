using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public enum MONSTERTYPE
    {
        MINION,
        MELEEMINION,
        VOIDWORM,
        OCKLEPOD,
        SIEGEMINION
    }
    public enum JOBTYPE
    {
        전사 = 1,
        마법사 = 2,
        도적 = 3
    }
    public enum LOBBYCHOICE
    {
        SAVE = 0,
        PLYAYERSTAT = 1,
        DENJEON = 2,
        POTION =3,
        INVENTORY =4,
        QUEST = 5
    }
    public enum ITEMTYPE
    {
        ARMOR,
        WEAPON,
        POTION
    }

    public enum EQUESTTYPE
    {
        MINION = 1,
        EQUIP
    }

    public enum DENJOENCHOICE
    {
        BATTLE = 1,
        INVENTORY = 2,
        POTION = 3,
        LOBBY = 0
    }

}
