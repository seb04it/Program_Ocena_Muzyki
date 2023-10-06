namespace Program_Ocena_Muzyki
{
    public class Statistics
    {
        public int Max { get; private set; }
        public int Min { get; private set; }
        public double Average { get; private set; }

        private List<int> ratings;

        public Statistics()
        {
            ratings = new List<int>();
            Max = int.MinValue;
            Min = int.MaxValue;
            Average = 0.0;
        }

        public void AddRating(int rating)
        {
            ratings.Add(rating);
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            if (ratings.Count == 0)
            {
                Max = int.MinValue;
                Min = int.MaxValue;
                Average = 0.0;
            }
            else
            {
                Max = ratings.Max();
                Min = ratings.Min();
                Average = ratings.Average();
            }
        }
    }
}
