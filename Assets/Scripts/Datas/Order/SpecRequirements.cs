namespace Data
{
    [System.Serializable]
    public class SpecRequirements
    {
        public bool checkLength;     // 長さをチェックするか
        public bool checkWeight;     // 重さをチェックするか
        public bool checkSharpness;  // 鋭さをチェックするか
        
        public int requiredLength;   // 必要な長さ（最小値）
        public int requiredWeight;   // 必要な重さ（最小値）
        public int requiredSharpness; // 必要な鋭さ（最小値）
    }
    
}

