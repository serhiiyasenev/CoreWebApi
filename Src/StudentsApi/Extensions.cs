using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApi
{
    public static class Extensions
    {

        public static (bool, string) IsScoreAcceptable(this int score)
        {
            if (score > 100 || score < 0)
            {
                return (false, $"Wrong score: {score}. Score should be between 0 and 100");
            }

            return (true, $"Score {score} is Acceptable");

        }

    }
}
