using System;
using System.Collections.Concurrent;
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
            HasStraightFlush() ? HandRank.StraightFlush :
            HasStraight() ? HandRank.Straight :
            HasFlush() ? HandRank.Flush :
            HasFullHouse() ? HandRank.FullHouse :
            HasFourOfAKind() ? HandRank.FourOfAKind :
            HasThreeOfAKind() ? HandRank.ThreeOfAKind :
            HasPair() ? HandRank.Pair :
            HandRank.HighCard;

        private bool HasFlush() =>
            cards.All(c => cards.First().Suit == c.Suit);

        private bool HasRoyalFlush() =>
            HasFlush() && cards.All(c => c.Value > CardValue.Nine);

        //  The Any LINQ method validates that there are dictionary items with a specified pair count value.
        private bool HasOfAKind(int num) => cards.ToKindAndQuantities().Any(c => c.Value == num);

        private bool HasPair() => HasOfAKind(2);
        private bool HasThreeOfAKind() => HasOfAKind(3);
        private bool HasFourOfAKind() => HasOfAKind(4);

        private bool HasFullHouse() => HasThreeOfAKind() && HasPair();

        //  The Zip and Skip LINQ methods are replaced by a custom extension method, SelectConsecutive
        //  Select consecutive works like LINQ Select, except it can evaluate two consecutive items in a collection
        //  This is done using a yield keyword, the source code is in EvalExtensions.cs
        private bool HasStraight() =>
            cards
                .OrderBy(card => card.Value)
                .SelectConsecutive((n, next) => n.Value + 1 == next.Value)
                .All(value => value);

        private bool HasStraightFlush() => HasStraight() && HasFlush();
    }
}
