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

        public IEnumerable<Card> Cards => cards;

        public void Draw(Card card) => cards.Add(card);

        public Card HighCard() =>
            cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);

        //  Optional
        //  The return early pattern can be replaced with ternary operators
        //  then shortened to an expression-bodied member.
        public HandRank GetHandRank() =>
            HasRoyalFlush() ? HandRank.RoyalFlush :
            HasFlush() ? HandRank.Flush :
            HandRank.HighCard;

        private bool HasFlush() =>
            cards.All(c => cards.First().Suit == c.Suit);

        public bool HasRoyalFlush() =>
            HasFlush() && cards.All(c => c.Value > CardValue.Nine);
    }
}
