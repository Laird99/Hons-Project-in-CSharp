namespace Blackjack
{
    class Player
    {
        public string _name;
        public double _chips;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double Chips
        {
            get { return _chips; }
            set { _chips = value; }
        }

    }
}
