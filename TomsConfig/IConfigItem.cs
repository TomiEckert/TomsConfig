namespace TomsConfig {
    public interface IConfigItem {
        public bool Equals(ConfigItem item);
        
        public string GetString();
        public int GetInt();
        public double GetDouble();
        public bool GetBool();
        public string[] GetStrings();
        public int[] GetInts();
        public double[] GetDoubles();
        public bool[] GetBools();
    }
}