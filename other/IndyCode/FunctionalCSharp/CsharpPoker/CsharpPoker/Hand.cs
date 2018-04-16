using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpPoker
{
    public class Hand
    {
        private readonly List<Card> cards = new List<Card>();

        public IEnumerable<Card> Cards { get { return cards; } }

        public void Draw(Card card)
        {
            cards.Add(card);
        }

        public Card HighCard()
        {
            return cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);

            //  OrderBy is also valid, but could use more resources than Aggregate:
            // return cards.OrderBy(c => c.Value).Last();
        }

        public HandRank GetHandRank()
        {
            if (HasRoyalFlush()) return HandRank.RoyalFlush;
            if (HasFlush()) return HandRank.Flush;
            return HandRank.HighCard;
        }

        //  A LINQ All method combined with First can check if all suits are the sam value
        private bool HasFlush()
        {
            return cards.All(c => cards.First().Suit == c.Suit);
        }

        //  A LINQ All method can determine if all cards are greater than Nine or [Ten, Jack, Queen, King, Ace ]
        public bool HasRoyalFlush()
        {
            return HasFlush() && cards.All(c => c.Value > CardValue.Nine);
        }
    }

    public enum HandRank
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }
}
