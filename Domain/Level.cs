using System;
using System.Collections.Generic;
using System.Text;

namespace DeepMiner.Domain
{
    public class Level
    {
        public string[] map { get; private set; }
        public DateTime time { get; private set; }
        public int score { get; private set; }

        public Level(string[] map, DateTime time, int score)
        {
            this.map = map;
            this.time = time;
            this.score = score;
        }

        private static string[] CreateRandomLevel(int w, int h)
        {
            var mapLevel = new List<string>();
            var blocks = new List<char>();

            var rand = new Random();

            CreateFirstLevel(w, h, mapLevel, blocks, rand);

            CreateSecondLevel(w, h, mapLevel, blocks, rand);

            return CreateThirdLevel(w, h, mapLevel, blocks, rand);
        }

        private static void CreateFirstLevel(int w, int h, List<string> mapLevel, List<char> blocks, Random rand)
        {
            CreateManySolidStrings(w, mapLevel, " ", 2);
            CreateManySolidStrings(w, mapLevel, "T", 2);

            for (var i = 1; i < 5; i++)
                blocks.Add('T');
            for (var i = 1; i < 30; i++)
                blocks.Add('R');

            blocks.Add('C');
            blocks.Add('C');
            blocks.Add(' ');

            for (int i = 1; i <= h / 3; i++)
            {

                var word = "";
                for (var j = 0; j < w; j++)
                {
                    var letter_num = rand.Next(0, blocks.Count);
                    word += blocks[letter_num];
                }
                mapLevel.Add(word);
            }
        }

        private static void CreateSecondLevel(int w, int h, List<string> mapLevel, List<char> blocks, Random rand)
        {
            for (var i = 1; i < 30; i++)
                blocks.Add('R');
            blocks.Add('I');
            blocks.Add('I');
            blocks.Add('I');
            blocks.Add('B');
            blocks.Add('B');

            for (int i = h / 3; i <= h * 2 / 3; i++)
            {
                var word = "";
                for (var j = 0; j < w; j++)
                {
                    var letter_num = rand.Next(0, blocks.Count);
                    word += blocks[letter_num];
                }
                mapLevel.Add(word);
            }
        }

        private static string[] CreateThirdLevel(int w, int h, List<string> mapLevel, List<char> blocks, Random rand)
        {
            blocks.Add('G');
            blocks.Add('G');
            blocks.Add('E');
            blocks.Add('D');
            blocks.Add('O');
            blocks.Add('B');

            for (int i = h * 2 / 3; i <= h; i++)
            {
                var word = "";
                for (var j = 0; j < w; j++)
                {
                    var letter_num = rand.Next(0, blocks.Count);
                    word += blocks[letter_num];
                }
                mapLevel.Add(word);
            }

            CreateManySolidStrings(w, mapLevel, " ", 2);
            CreateManySolidStrings(w, mapLevel, "F", 3);
            return mapLevel.ToArray();
        }

        private static void CreateManySolidStrings(int w, List<string> mapLevel, string block, int max)
        {
            for (var i = 0; i < max; i++)
                mapLevel.Add(CreateSolidString(w, block));
        }

        private static string CreateSolidString(int w, string block)
        {
            var solidString = "";
            for (var i = 0; i < w; i++)
                solidString += block;
            return solidString;
        }

        public static IEnumerable<Level> LoadLevels()
        {
            yield return new Level(new string[]
            {
                "           ",
                "           ",
                "TTTTTTTTTTT",
                "TTTTTTTTTTT",
                "RRRRRRRRRRR",
                "RRRRRCRRRRR",
                "ORRROOORRRR",
                " RCRRIRRCR ",
                "RRRROOOR RO",
                "RRRRRCRRRRR",
                "R RRRRRRR R",
                "RRRRR RRRRR",
                " RGRRRRRCRR",
                "ORRRORORRRR",
                "R RRODORR  ",
                "RRRROOORRRO",
                "RRCRRRRRGRR",
                "RRRRRRRRRRR",
                "RRRRRRRRERR",
                " ROR ROR R ",
                "RR ROR RORR",
                "D  R R R  R",
                " RORСRORCR ",
                "RR ROR RORR",
                "R CR RIR ER",
                "           ",
                "           ",
                "FFFFFFFFFFF",
                "FFFFFFFFFFF",
                "FFFFFFFFFFF",
            },
            new DateTime().AddMinutes(1.5), 100);

            yield return new Level(new string[] 
            {
                "           ",
                "           ",
                "TTTTTTTTTTT",
                "TTTTRTRTTTT",
                "RCRRBTBTRRT",
                "R RRORORROR",
                "RRBRRRRRRIR",
                "RRRRORORRRR",
                "RR RRBRBRRR",
                "R RRCRGRRRR",
                "RRRR R R RR",
                "RIBRR R RIR",
                "R RRCREIRRR",
                "RRRRRRRRRRR",
                "RRRRRRRRRRR",
                "RRCIORRRRGR",
                "RRRRORRRC R",
                "RERRORRRRRR",
                "RBRRORRRCCR",
                "RERRRRRRRRR",
                " R R R R R ",
                "R B R B R R",
                "RRRRRRRRGRR",
                " R R R R R ",
                "R B R B R R",
                "RBRBRBRRDRR",
                "CRRRRRRDBDR",
                "RRRRRRRRRRR",
                "RRRRRRRORRR",
                "RRRERBBBRBB",
                "RRR RRRRDRR",
                "           ",
                "           ",
                "FFFFFFFFFFF",
                "FFFFFFFFFFF",
                "FFFFFFFFFFF",

            },
            new DateTime().AddMinutes(2), 150);

            yield return new Level(new string[]
            {
                "             ",
                "             ",
                "TTTTTTTTTTTTT",
                "TTTTTTTTTTTTT",
                "RR RR R R RRR",
                "RRORRRRRRORRR",
                "RRBBRRRRRCRRR",
                "RRCRRRORRRIRR",
                "RR RRRORRI RR",
                "RBRRCRORRRBRR",
                "RRIR RORIRRRR",
                "RRRRRRORRRRRR",
                "RRRRRRRRRRRRR",
                "RRRRGRRRRRRRR",
                "RRRR RRRRR RR",
                "RR R IRR RBRR",
                "RRBRBRRRBRRRR",
                "RRRRORRORRRRR",
                "RRRRORRORRGRR",
                "RRGROREORRRRR",
                "RRRROROORRRRR",
                "RRO RRRRRRCRR",
                "RRRRORRRRRRRR",
                "RRRRRRRRRRRRR",
                "RDRRERRRRIRRR",
                "RBRR RRRRRRRR",
                "RRRRBRRRRRRRR",
                "RRRRRRROGORRR",
                "RRRRRRRDORRRR",
                "RRRRRRROORRRR",
                "R R R R ORROR",
                "ROROROO ORROR",
                "RRRRRRRDRRRRR",
                "             ",
                "             ",
                "FFFFFFFFFFFFF",
                "FFFFFFFFFFFFF",
                "FFFFFFFFFFFFF",
            },
            new DateTime().AddMinutes(3), 200);

            yield return new Level(CreateRandomLevel(15, 30),
            new DateTime().AddMinutes(5), 500);
        }
    }
}

