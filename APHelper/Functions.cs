using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace APHelper.Functions;

public class APHelperFunctions
{
    public static Data_Item GetItemById(string itemId)
    {
        Hashtable_Items hashtable = Hashtable_Items.getHashtable;
        if (hashtable == null) {
            Melon<APHelperClass>.Logger.Msg("Hashtable_Items.getHashtable is null.");
        }
        Data_Item item = hashtable.GetItemByID(itemId);
        if (item == null) {
            Melon<APHelperClass>.Logger.Msg($"Could not find item with id '{itemId}'.");
        }
        return item;
    }
}