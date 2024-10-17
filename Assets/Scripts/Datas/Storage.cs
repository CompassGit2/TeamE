using System.Collections.Generic;

namespace Data{
    public static class Storage
    {
        public static List<MaterialStack> materials = new List<MaterialStack>();

        public static void AddItem(MaterialData materialData, int amount)
        {
            // アイテムがすでにあるか確認
            MaterialStack stack = materials.Find(i => i.material == materialData);
            
            if (stack != null)
            {
                // 既存のアイテムがあれば所持数を増やす
                stack.amount += amount;
            }
            else
            {
                // なければ新しいアイテムを追加
                materials.Add(new MaterialStack(materialData, amount));
            }
        }

        public static void RemoveItem(MaterialData materialData, int amount)
        {
            MaterialStack stack = materials.Find(i => i.material == materialData);

            if (stack != null)
            {
                stack.amount -= amount;

                if (stack.amount <= 0)
                {
                    materials.Remove(stack); // 所持数が0以下になったら削除
                }
            }
        }
    }
}