using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using TMPro;

public enum WearableType
{
    None,
    Hat,
    Back,
    Waist,
    Weapon,
    Neck,
    Skin
}
public class MetaStringData
{
    public static List<string> bodyList = new List<string>() { "ANGEL", "ASSASSIN", "ASTRONAUT", "BIKER", "BLOODIED", "BREAD", "CAMOUFLAGE", "COUNTRY", "COWBOY", "DEMON", "EXPLORER", "GALAXY", "GENTLEMAN", "GOTHGIRL", "LIFEGUARD", "LIGHTBLUE", "LIZARD", "LUMBERJACK", "METABOY", "NEON SOLID", "NEON TRANSPARENT", "MOMJEAN", "MONK", "MONSTERSUIT", "MUSCULARWARRIOR", "NOBLEMAN", "OPENJACKET", "PRISONER", "REDDRESS", "ROMANGLADIATOR", "SAMURAI", "SANTACLAUS", "SCHOOLBOY", "SCHOOLGIRL", "STEALTH", "STONE", "STRIPPED", "SUIT", "SUPERHERO", "SUPERVILLAIN", "SURVIVOR", "SUSPENDERS", "TIGER", "TURTLE", "UNDERWATERSUIT", "WATER", "WATERMELON", "WOOD", "YELLOW", "YELLOWOVERALLS", "ZOMBIE" };

    public static List<string> faceList = new List<string>() { "6_6", "404", "ANGEL SMILE", "ANGRY", "BEARDED COWBOY", "BIG CHIN", "BIG MOUSTASHE", "BIG TEETH", "CAT", "HAPPY CLOWN", "CONFUSED", "CRAZY", "CRYING", "CYCLOPS", "DEMON", "DOG", "DOUBLE CHIN", "DUMB LAUGH", "EMPTY FACE", "FEMALE VILLAIN", "FURIOUS", "GALAXY", "GLASSES", "GOATEE", "GOT YOU", "GRINDING TEETH", "HANDSOME FACE", "HAPPY BOY", "HAPPY GIRL", "HITMAN", "HOLDING A LAUGH", "HUGE SMILE", "LOUD CRYING", "METABOY", "MONOCLE", "MONSTER", "MUSTASHE HIPSTER", "NEON", "OOO", "PIG", "ROSY CHEEKS", "SAD", "SAD ZOMBIE", "SCARY CLOWN", "SEDUCING", "SERIOUS", "SKULL", "SMILING SKULL", "SMILING ZOMBIE", "SNOBBY KID", "SUPER ANGRY", "SURPRISED", "SWEET", "TEETH SMILE", "TONGUE OUT", "UNIBROW BANDIT", "VIPER", "WHATEVER" };

    public static List<string> hatList = new List<string>() {"NONE", "ARROW THROUGH HEAD", "ASTRONAUT HELMET", "AXE HAT", "BEAR HAT", "BERET", "BITCOIN HAT", "BUNNY EARS", "CAT EARS", "CHEFS HAT", "COWBOY HAT", "CROWN", "DARK HOODIE", "DARK PURPLE WIZARD HAT", "DETECTIVE HAT", "ETHEREUM HAT", "FEDORA", "FEDORA METABOY", "GREEN HEADPHONES", "GREEN MOHAWK", "HALO", "HAT BLUE", "HAT ORANGE", "JESTER HAT", "LEAFS", "LION HAT", "LOOPRING HAT", "MEDIC HAT", "PIRATE HAT", "PLAGUE DOCTOR HAT", "PLAGUE DOCTOR MASK", "PURPLE MOHAWK", "PURPLE WIZARD HAT", "RED MOHAWK", "RIBBON", "RUSSIAN HAT", "SAMURAI HELMET", "SANTA HAT", "SKULL TOP HAT", "SEA CAPTAIN HAT", "STEAMPUNK HAT", "UNDERWATER HELMET", "VIKING HELMET", "WARRIOR HELMET", "WHITE MOHAWK", "WINGED HELMET", "WIZARD HAT", "YELLOW HAT" };

    public static List<string> weaponList = new List<string>() { "NONE","AXE AND SHIELD", "BAZOOKA", "BLADE", "BOMB", "BOW", "BOW AND ARROW", "BOXING GLOVES", "CHAINSAW", "COWBOY LEFT PISTOL", "COWBOY RIGHT PISTOL", "COWBOY TWO PISTOLS", "CROWBAR", "DAGGERS", "DARK STAFF", "DYNAMITE STICK", "ELDER WAND", "ENERGY SWORD", "FLAMETHROWER", "GOLDEN BLADES", "GOLDEN SWORD", "GRAVITY GUN", "HARPOON","HOOK","KATANA","KUNAI","LASER GUNS", "LIGHTNING", "MEDUSAS HEAD", "NEON SOLID", "NEON TRANSPARENT", "RETRO FUTURISTIC RIFLE", "ROBOT CLAW", "SAI", "SPEAR AND SHIELD", "SIDE GUN", "SLINGSHOT", "SNAIL SHELL", "SNIPER", "TRIDENT", "WOODEN STAFF", "WRIST STRAPS", "YATAGAN" };

    public static List<string> backList = new List<string>() { "NONE", "ANGEL WINGS", "DEMON WINGS", "GOLDEN WINGS", "SAI BACKPACK", "SWORD BACKPACK", "METABOY BACKPACK" };

    public static List<string> neckList = new List<string>() { "NONE", "ARROW QUIVER", "BINOCULARS", "BLACK SCARF", "BLUE BANDANA", "DOG TAGS", "FUR CAPE", "MEDALLION NECKLACE", "RED BANDANA", "RED CAPE", "RED SCARF", "SHOULDER PADS", "SKULL BANDANA", "TEETH NECKLACE" };

    public static List<string> waistList = new List<string>() { "NONE", "BELT EMPTY HOLSTERS", "BELT WITH BOTH PISTOLS", "BELT WITH LEFT PISTOL", "BELT WITH RIGHT PISTOL", "CHAIN BELT", "CHAMPION BELT", "FIGHTER BELT", "GRENADE BELT", "POTION BELT", "SATCHEL", "WARRIOR BELT" };
}

public class ListToPopupAttribute : PropertyAttribute
{
    public Type myType;
    public string propertyName;

    public ListToPopupAttribute(Type _myType, string _propertyName)
    {
        myType = _myType;
        propertyName = _propertyName;
    }
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ListToPopupAttribute))]
public class ListToPopupDrawer : PropertyDrawer
{
    int selectedIndex = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ListToPopupAttribute atb = attribute as ListToPopupAttribute;
        List<string> stringList = null;

        if (atb.myType.GetField(atb.propertyName) != null)
        {
            stringList = atb.myType.GetField(atb.propertyName).GetValue(atb.myType) as List<string>;
        }
        if (stringList != null && stringList.Count != 0)
        {
            selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
            property.stringValue = stringList[selectedIndex];
        }
        else EditorGUI.PropertyField(position, property, label);
    }
}
#endif
public static class TMP_TextExtender
{
    public static void SetText(this TMP_Text text, UnityEngine.Object inText)
    {
        text.text = inText.ToString();
    }

    public static void SetText(this TMP_Text text, UnityEngine.Object inText, Color inColor)
    {
        text.text = "<#" + ColorUtility.ToHtmlStringRGB(inColor) + ">" + inText.ToString() + "</color>";
    }

    public static void SetTextIndented(this TMP_Text text, string inText, int inIndentAmount)
    {
        text.text = "<line-indent=" + inIndentAmount.ToString() + "%>" + inText + "</line-indent>";
    }

    public static string Colorize(this string textString, Color inColor)
    {
        return "<#" + ColorUtility.ToHtmlStringRGB(inColor) + ">" + textString + "</color>";
    }

    public static string Bold(this string textString)
    {
        return "<b>" + textString + "</b>";
    }

    public static string Italic(this string textString)
    {
        return "<i>" + textString + "</i>";
    }
}


public static class TransformDeepChildExtension
{
    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }

    /*
    //Depth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        foreach(Transform child in aParent)
        {
            if(child.name == aName )
                return child;
            var result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }
    */
}

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
