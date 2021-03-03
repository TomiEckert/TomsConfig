namespace TomsConfig {
    public interface IParser {
        internal void SetValue(string value);
        
        public int GetInt();

        public string GetString();

        public double GetDouble();

        public bool GetBool();

        public string[] GetStrings();

        public int[] GetInts();

        public double[] GetDoubles();

        public bool[] GetBools();
    }
}