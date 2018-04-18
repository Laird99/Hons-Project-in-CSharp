namespace Blackjack
{
    class Card
    {
        public string _face;
        public int _value;
         
        public string Face {
            get { return _face;  }
            set { _face = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}
