namespace AoC2019.Problems.Day11
{
    public class Move
    {
        public TurnDirection Turn { get; }
        public PaintColor Color { get; }

        public Move(PaintColor color, TurnDirection turn)
        {
            this.Turn = turn;
            this.Color = color;
        }

        public override string ToString()
        {
            return $"{Turn.ToString()} --> {Color.ToString()}";
        }
    }
}
