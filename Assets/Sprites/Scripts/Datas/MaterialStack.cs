namespace Data
{
    [System.Serializable]
    public class MaterialStack
    {
        public MaterialData material;
        public int amount;

        public MaterialStack(MaterialData materialData, int amount)
        {
            this.material = materialData;
            this.amount = amount;
        }
    }
    
}
