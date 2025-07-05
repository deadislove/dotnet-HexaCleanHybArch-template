namespace HexaCleanHybArch.Template.Tests.Integration.Common
{
    public static class TestDataGenerator
    {
        private static readonly Random _random = new();

        public static string GenerateRandomEmail(string domain = "test.com")
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string user = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[_random.Next(s.Length)]).ToArray());

            return $"{user}@{domain}";
        }

        public static DateTime GenerateRandomDate(DateTime? from = null, DateTime? to = null)
        {
            from ??= new DateTime(2000, 1, 1);
            to ??= DateTime.Today;

            var range = (to.Value - from.Value).Days;
            return from.Value.AddDays(_random.Next(range));
        }

        public static DateTime GenerateRandomBirthday(int minAge = 18, int maxAge = 100)
        {
            if (minAge < 0 || maxAge < minAge)
                throw new ArgumentException("Invalid age range");

            DateTime today = DateTime.Today;
            DateTime maxDate = today.AddYears(-minAge);
            DateTime minDate = today.AddYears(-maxAge);

            int range = (maxDate - minDate).Days;
            return minDate.AddDays(_random.Next(range));
        }
    }
}
