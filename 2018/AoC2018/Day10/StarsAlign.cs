using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using AoC.Common;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day10
{
    public class StarsAlign :AoCSolution<string>
    {
        private static string outputFilePath = @"e:\tmpImages\";
        

        /// <summary>
        /// Had to fudge the output as the search area is too big for the console window.
        /// So instead we generate a bitmap and save in the outputfilePath folder.
        /// You'll need to manually check these images to find the result.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            List<StarPosition> stars = input.Select(x => StarPosition.CreateStarPosition(x)).ToList();
           RunStarMapSimulation(stars);
           yield return "Starmap simulation finished!  Images saved in: " + outputFilePath;
        }

        public override int Year => 2018;
        public override int Day => 10;
        public override string Name => "Day 10: The Stars Align";
        public override string InputFileName => "Day10.txt";


        /// <summary>
        /// Step through the simulation.
        /// Could use a binary search - but its easier to just increment by 10, and then drop down to steps of 1 when the boundary is small enough 
        /// </summary>
        /// <param name="stars"></param>
        public void RunStarMapSimulation(ICollection<StarPosition> stars)
        { 
            int maxBoundary = 250;  // only check layouts where the boundary of the starmap (ie. width and height) are less than this.
            Console.WriteLine(Console.LargestWindowWidth + " - " + Console.LargestWindowHeight);
            int originalStepSize = 10;
            int stepSize = originalStepSize;

            for (int i = 0; i <= 15000; i+= stepSize)
            {
                
                int minX = stars.Min(s => s.Position.X);
                int minY = stars.Min(s => s.Position.Y);

                int maxX = stars.Max(s => s.Position.X);
                int maxY = stars.Max(s => s.Position.Y);


                int width = maxX - minX;
                int height = maxY - minY;


                if (width <= maxBoundary && height <= maxBoundary)
                {
                    stepSize = 1;
                    CreateImage(stars,i);
                   
                }
                else
                {
                    stepSize = originalStepSize;
                }

                foreach (var star in stars)
                {
                    star.Move(stepSize);
                }

                Console.WriteLine($"Step {i} => Width = {width}, height = {height}");
            }
        }


        private void CreateImage(IEnumerable<StarPosition> stars, int id)
        {

            int minX = stars.Min(s => s.Position.X);
            int minY = stars.Min(s => s.Position.Y);

            int maxX = stars.Max(s => s.Position.X);
            int maxY = stars.Max(s => s.Position.Y);


            int width = maxX - minX + 10;
            int height = maxY - minY + 10;

            Bitmap img = new Bitmap(1000, 1000);

            using (Graphics gfx = Graphics.FromImage(img))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                gfx.FillRectangle(brush, 0, 0, width, height);
            }

           
            foreach (var star in stars)
            {
                int x = star.X - minX;
                int y = star.Y - minY;

                
                img.SetPixel(x, y, Color.White);
            }

            // String concat is bad - but i'm hardcoded values anyway...
            string fileName = outputFilePath + id.ToString() + ".bmp";
            img.Save(fileName);

        }
    }


    public class StarPosition
    {
        private static readonly string pattern = @"^position=<\s*(?<posX>[0-9-]+),\s*(?<posY>[0-9-]+)\s*>\s*velocity=<\s*(?<velocityX>[0-9-]+)\s*,\s*(?<velocityY>[0-9-]+)>\s*$";
        public Position Position { get; private set; }
        public int VelocityX { get;}
        public int VelocityY { get; }

        public int X => Position.X;
        public int Y => Position.Y;

        public StarPosition(int x, int y, int velocityX, int velocityY)
        {
            Position = new Position(x, y);
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public static StarPosition CreateStarPosition(string input)
        {
            Match match = Regex.Match(input, pattern);
            return new StarPosition(int.Parse(match.Groups["posX"].Value),
                int.Parse(match.Groups["posY"].Value),
                int.Parse(match.Groups["velocityX"].Value),
                int.Parse(match.Groups["velocityY"].Value));
        }

        public void Move(int steps = 1)
        {
            Position p = new Position(Position.X + (VelocityX * steps), Position.Y + (steps * VelocityY));
            this.Position = p;
        }

        public void MoveBack(int steps = 1)
        {
            Position p = new Position(Position.X - (VelocityX * steps), Position.Y - (VelocityY * steps));
            this.Position = p;
        }
    }
}
