namespace Data
{
    public static class PlayerData
    {
        public static int WorldRank
        {
            get => worldRank;
        }

        private static int worldRank = 0;

        public static void SetWorldRank(int rank)
        {
            worldRank = rank;
        }
    }

}
