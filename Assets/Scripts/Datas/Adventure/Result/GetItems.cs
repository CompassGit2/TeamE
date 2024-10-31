using System.Collections.Generic;
using Data;
using UnityEngine;

namespace ResultScene
{
    public static class GetItems
    {
        public static List<MaterialStack> MaterialStacks
        {
            get => _materialStacks;
        }
        static List<MaterialStack> _materialStacks = new();

        public static void AddItems(List<MaterialStack> materialStacks)
        {
            foreach (var stack in materialStacks)
            {
                _materialStacks.Add(new MaterialStack(stack.material, stack.amount));
            }
        }
        public static void RemoveItems()
        {
            _materialStacks = new List<MaterialStack>();
        }

        
    }

}
